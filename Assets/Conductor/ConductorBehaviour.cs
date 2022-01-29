using UnityEngine;
using UnityEngine.Events;

namespace MJam22.Conductor
{
    public class ConductorBehaviour : MonoBehaviour
    {
        public UnityEvent<int> OnNewBeat = new UnityEvent<int>();
        public UnityEvent<float> OnUpdatedSongTemp = new UnityEvent<float>();
        
        [SerializeField] float firstBeatOffset = 0;
        [SerializeField] float bpm = 62;

        [SerializeField] AudioSource musicSource;

        Conductor conductor;
        float beatOffset;

        void Awake()
        {
            conductor = new Conductor(bpm, musicSource, OnNewBeat, OnUpdatedSongTemp);
        }

        void Start()
        {
            conductor.StartSong();
            beatOffset = firstBeatOffset;
        }

        void FixedUpdate()
        {
            beatOffset = firstBeatOffset + Time.fixedDeltaTime;
            UpdateSongPosition();
        }

        void UpdateSongPosition()
        {
            conductor.UpdateSongPosition(beatOffset);
        }
    }
}