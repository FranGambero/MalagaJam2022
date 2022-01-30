using System.Collections.Generic;
using System.Linq;
using MJam22.Beat;
using MJam22.Conductor;
using MJam22.Stress;
using UnityEngine;
using UnityEngine.UI;

namespace MJam22.States
{
    public class StateController : MonoBehaviour
    {
        [SerializeField] List<StateDataModelScriptable> states;
        [SerializeField] BackgroundController.BackgroundController backgroundController;
        [SerializeField] ConductorBehaviour conductorBehaviour;
        [SerializeField] CycleTimeController cycleTimeController;
        [SerializeField] StressBehaviour stressBehaviour;
        [SerializeField] BeatTracksManager tracksManager;

        [Header("Note Prefabs")] 
        [SerializeField] GameObject notePrefabOffice;
        [SerializeField] GameObject notePrefabDrag;
        State currentState;


        #region Load
        public void LoadState(int currentCycle) => LoadState(states[currentCycle].Data, currentCycle);

        public void LoadState(StateDataModel state, int currentCycle)
        {
            LoadAudio(state);
            LoadCycleTime(state.duration);
            LoadView(state.isOffice, currentCycle);
            LoadStress(state.timedStress, state.hitStress, state.missStress);
            LoadTracks(new List<List<float>>{state.FirstTrack, state.SecondTrack, state.ThirdTrack, state.FourthTrack}, state.secondsToArrive, state.isOffice);
        }

        void LoadAudio(StateDataModel state)
        {
            conductorBehaviour.LoadNewTrack(state.clip, state.bpm, state.firstBeatOffset);
        }

        void LoadCycleTime(int duration) => cycleTimeController.SetCycleTime(duration);

        void LoadView(bool isOffice, int currentCycle)
        {
            //TODO: meter aqui el animator correspondiente y hacerle las animaciones
            backgroundController.ChangeStatus(isOffice, currentCycle);
        }

        void LoadStress(float timedStress, float hitStres, float missStress)
        {
            stressBehaviour.SetTimeStress(timedStress);
            stressBehaviour.SetHitStress(hitStres);
            stressBehaviour.SetMissStress(missStress);
        }

        void LoadTracks(List<List<float>> tracks, float speed, bool isOffice)
        {
            for(var i = 0; i < tracksManager.TrackControllers.Count; i++)
            {
                tracksManager.TrackControllers[i].SetNotes(tracks[i].ToList());
                tracksManager.TrackControllers[i].SetNoteSpeed(speed);
                tracksManager.TrackControllers[i].SetNotePrefab(isOffice ? notePrefabOffice : notePrefabDrag);
            }
        }
        #endregion
        
        #region Launch
        public void LaunchCurrentState()
        {
            conductorBehaviour.StartConductor();
            tracksManager.StartTracks();
            cycleTimeController.StartCycle();
            stressBehaviour.StoptimedStress();
            stressBehaviour.StartTimedStress();
        }
        #endregion
    }
}