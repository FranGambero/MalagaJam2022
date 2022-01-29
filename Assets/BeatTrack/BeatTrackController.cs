using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils;

namespace MJam22.Beat
{
    public class BeatTrackController : MonoBehaviour
    {
        [SerializeField] KeyCode inputKey;
        public float secondsToArrive;
        [SerializeField] GameObject NotePrefab;
        [SerializeField] Transform NoteSpawnPosition;
        [SerializeField] Transform NoteEndPosition;
        
        [SerializeField] List<float> notesToSpawn;

        NoteHolder noteHolder;
        
        #region Events
        public readonly NoteBehaviourUnityEvent onNoteOutOfSight = new NoteBehaviourUnityEvent();
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

        void InitListeners()
        {
            onNoteOutOfSight.AddListener(FailNote);
        }

        void MoveNotes()
        {
            noteHolder.MoveNotes(secondsToArrive);
        }

        #region Spawn
        public void TrySpawnNote(float songPosSec)
        {
            TrySpawnNote(songPosSec, secondsToArrive);
        }
        
        void TrySpawnNote(float songPosSec, float travelSeconds)
        {
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
            if(Input.GetKeyDown(inputKey))
                ClearNotes();
        }
        
        void ClearNotes()
        {
            var notesToClear = noteHolder.GetActivatedNotes().ToList();
            Debug.Log($"{notesToClear.Count} notes to clear");

            if(notesToClear.Any())
            {
                var note = notesToClear.First();
                RemoveNote(note);
            }
        }

        void FailNote(NoteBehaviour note)
        {
            RemoveNote(note);
            Debug.Log("Aumenta el estres");
        }

        void RemoveNote(NoteBehaviour note)
        {
            noteHolder.RemoveNotes(note);
            Destroy(note.gameObject);
        }
        #endregion
        
        #region SupportMethods
        
        #endregion
    }   
}
