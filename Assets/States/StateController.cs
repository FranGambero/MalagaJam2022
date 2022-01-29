using System.Collections.Generic;
using MJam22.Beat;
using MJam22.Conductor;
using MJam22.Stress;
using UnityEngine;
using UnityEngine.UI;

namespace MJam22.States
{
    public class StateController : MonoBehaviour
    {
        [SerializeField] List<StateDataModel> states;
        [SerializeField] ConductorBehaviour conductorBehaviour;
        [SerializeField] CycleTimeController cycleTimeController;
        [SerializeField] StressBehaviour stressBehaviour;
        [SerializeField] BeatTracksManager tracksManager;
        [SerializeField] Image background;
        
        State currentState;

        public void Init()
        {
            currentState = new OfficeState(stressBehaviour, background);
            currentState.Start();
        }

        public void ChangeState()
        {
            if(currentState.GetType() == typeof(OfficeState))
            {
                currentState = new ClubState(stressBehaviour, background);
                currentState.Start();                
            }
            else if(currentState.GetType() == typeof(ClubState))
            {
                currentState = new OfficeState(stressBehaviour, background);
                currentState.Start();
            }
        }

        #region Load
        public void LoadState(StateDataModel state)
        {
            LoadCycleTime(state.duration);
            LoadView(state.isOffice);
            LoadStress(state.timedStress, state.hitStress, state.missStress);
            LoadTracks(new List<List<float>>{state.FirstTrack, state.SecondTrack, state.ThirdTrack, state.FourthTrack});
        }

        void LoadCycleTime(int duration) => cycleTimeController.SetCycleTime(duration);

        void LoadView(bool isOffice)
        {
            //TODO: meter aqui el animator correspondiente y hacerle las animaciones
        }

        void LoadStress(float timedStress, float hitStres, float missStress)
        {
            stressBehaviour.SetTimeStress(timedStress);
            stressBehaviour.SetHitStress(hitStres);
            stressBehaviour.SetMissStress(missStress);
        }

        void LoadTracks(List<List<float>> tracks)
        {
            for(var i = 0; i < tracksManager.TrackControllers.Count; i++)
            {
                tracksManager.TrackControllers[i].SetNotes(tracks[i]);
            }
        }
        #endregion
        
        #region Launch
        public void LaunchCurrentState()
        {
            conductorBehaviour.StartConductor();
            tracksManager.StartTracks();
        }
        #endregion
    }
}