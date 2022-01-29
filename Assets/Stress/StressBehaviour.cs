using System.Collections.Generic;
using MJam22.Beat;
using UnityEngine;
using UnityEngine.Events;

namespace MJam22.Stress
{
    public class StressBehaviour : MonoBehaviour
    {
        [SerializeField] float maxStress;
        [SerializeField] StressView stressView;

        [SerializeField] List<BeatTrackController> beatTrackControllers;

        StressController stressController;
        UnityEvent OnMaxStress = new UnityEvent();

        float timedStress = 1;
        float missStress = 10;
        float hitStress = 10;

        void Start()
        {
            stressController = new StressController(maxStress, OnMaxStress);
            InitListeners();
            ViewStress();
        }

        void InitListeners()
        {
            foreach(var beatTrackController in beatTrackControllers)
            {
                beatTrackController.onNoteOutOfSight.AddListener((NoteBehaviour)=>AddFailStress());
            }
            
            OnMaxStress.AddListener(()=>Debug.Log("Max Stress"));
        }

        public void SetTimeStress(float timedStress) => this.timedStress = timedStress;
        public void SetMissStress(float failStress) => this.missStress = failStress;
        public void SetHitStress(float hitStress) => this.hitStress = hitStress;
        void ViewStress() => stressView.FillBar(stressController.Stress);
        
        void AddFailStress()
        {
            stressController.IncreaseStress(missStress);
            ViewStress();
        }
    }
}