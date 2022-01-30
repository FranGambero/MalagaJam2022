using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Utils;

namespace MJam22.Beat
{
    public class BeatTrackController : MonoBehaviour
    {
        float secondsToArrive;

        [SerializeField] KeyCode inputKey;
        [SerializeField] BeatTrackButtonView buttonView;
        [SerializeField] GameObject NotePrefab;
        [SerializeField] Transform NoteSpawnPosition;
        [SerializeField] Transform NoteEndPosition;
        [SerializeField] List<float> notesToSpawn;
        [SerializeField] ParticleSystem hitParticles;

        NoteHolder noteHolder;
        bool isActive = false;
        
        #region Events
        public readonly NoteBehaviourUnityEvent onNoteOutOfSight = new NoteBehaviourUnityEvent();
        public readonly UnityEvent onHitNote = new UnityEvent();
        #endregion

        void Awake()
        {
            var distance = NoteEndPosition.transform.position.y - NoteSpawnPosition.transform.position.y;
            noteHolder = new NoteHolder(distance);
        }

        void Start()
        {
            InitListeners();
        }

        void Update()
        {
            MoveNotes();
            TryClearNotes();
        }

        public void StartTrack() => isActive = true;
        public void StopTrack() => isActive = false;

        public void SetNotes(List<float> notes) => notesToSpawn = notes;
        public void SetNoteSpeed(float secondsToArrive) => this.secondsToArrive = secondsToArrive;
        public void SetNotePrefab(GameObject notePrefab) => this.NotePrefab = notePrefab;

        void InitListeners()
        {
            onNoteOutOfSight.AddListener(FailNote);
        }

        void MoveNotes()
        {
            if(!isActive)
                return;
            noteHolder.MoveNotes(secondsToArrive);
        }

        #region Spawn
        public void TrySpawnNote(float songPosSec)
        {
            if(!isActive)
                return;
            
            TrySpawnNote(songPosSec, secondsToArrive);
        }
        
        void TrySpawnNote(float songPosSec, float travelSeconds)
        {
            if(!notesToSpawn.Any())
                return;
            
            var nextNoteBeat = notesToSpawn.First();
            var travelBeat = travelSeconds / (60 / 126f);
            if((songPosSec + travelBeat) >= nextNoteBeat)
            {
                Debug.Log($"Next note Beat: {nextNoteBeat}");
                Debug.Log($"Estimated Beat: {songPosSec+travelBeat}");
                Debug.Log($"Spawn time: {songPosSec}");
                notesToSpawn.RemoveAt(0);
                SpawnNote();
            }
        }

        void SpawnNote()
        {
            var note = Instantiate(NotePrefab, NoteSpawnPosition).GetComponent<NoteBehaviour>();
            note.SetOnOutOfSight(onNoteOutOfSight);
            noteHolder.AddNote(note);
        }
        #endregion

        #region Clear
        void TryClearNotes()
        {
            if(!isActive)
                return;

            if(Input.GetKeyDown(inputKey))
            {
                ValidateNotes();
                buttonView.Press();                
            }

            if(Input.GetKeyUp(inputKey))
            {
                buttonView.UnPress();
            }
        }
        
        void ValidateNotes()
        {
            var notesToClear = noteHolder.GetActivatedNotes().ToList();
            Debug.Log($"{notesToClear.Count} notes to clear");

            if(notesToClear.Any())
            {
                var note = notesToClear.First();
                RemoveNote(note);
                onHitNote.Invoke();
                hitParticles.Play();
                return;
            }

            var aliveNotes = noteHolder.GetAliveNotes().ToList();
            if(aliveNotes.Any())
            {
                onNoteOutOfSight.Invoke(null);
            }
        }

        void FailNote(NoteBehaviour note)
        {
            if(note == null)
                return;
            RemoveNote(note);
            Debug.Log("Aumenta el estres");
        }

        void RemoveNote(NoteBehaviour note)
        {
            noteHolder.RemoveNotes(note);
            Destroy(note.gameObject);
        }
        #endregion
    }   
}
