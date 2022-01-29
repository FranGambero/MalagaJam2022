using System.Collections.Generic;
using UnityEngine;

namespace MJam22.Beat
{
    public class BeatTracksManager : MonoBehaviour
    {
        public List<BeatTrackController> TrackControllers => trackControllers;
        [SerializeField] List<BeatTrackController> trackControllers;
        
        public void StartTracks()
        {
            foreach(var trackController in trackControllers)
            {
                trackController.StartTrack();
            }    
        }

        public void StopTracks()
        {
            foreach(var trackController in trackControllers)
            {
                trackController.StopTrack();
            }
        }
    }
}