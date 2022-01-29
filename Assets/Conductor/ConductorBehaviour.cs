using UnityEngine;
using UnityEngine.Events;

namespace MJam22.Conductor
{
    public class ConductorBehaviour : MonoBehaviour
    {
        UnityEvent<int> OnNewBeat = new UnityEvent<int>();
        public UnityEvent<float> OnUpdatedSongTemp = new UnityEvent<float>();
        
        [SerializeField] float firstBeatOffset = 0;
        [SerializeField] float bpm = 62;

        [SerializeField] AudioSource musicSource;

        Conductor conductor;
        float beatOffset;
        bool isActive = false;

        void Awake()
        {
            conductor = new Conductor(bpm, musicSource, OnNewBeat, OnUpdatedSongTemp);
        }

        void FixedUpdate()
        {
            UpdateSongPosition();
        }

        public void StartConductor()
        {
            conductor.StartSong();
            beatOffset = firstBeatOffset;
            
            isActive = true;
        }

        public void StopConductor()
        {
            conductor.StopSong();
            
            isActive = false;
        }

        void UpdateSongPosition()
        {
            if(!isActive)
                return;
            beatOffset = firstBeatOffset + Time.fixedDeltaTime;
            conductor.UpdateSongPosition(beatOffset);
        }
    }
}