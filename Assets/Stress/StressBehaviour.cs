using System.Collections;
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
        Coroutine timedCoroutine;
        UnityEvent OnMaxStress = new UnityEvent();

        float timedStress;
        float missStress;
        float hitStress;

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
                beatTrackController.onNoteOutOfSight.AddListener((NoteBehaviour)=>IncreaseByFailStress());
                beatTrackController.onHitNote.AddListener(ReduceByHitStress);
            }
            
            OnMaxStress.AddListener(()=>Debug.Log("Max Stress"));
        }

        public void SetTimeStress(float timedStress) => this.timedStress = timedStress;
        public void SetMissStress(float failStress) => this.missStress = failStress;
        public void SetHitStress(float hitStress) => this.hitStress = hitStress;
        public void StartTimedStress()
        {
            timedCoroutine = StartCoroutine(StressByTime());
        }

        public void StoptimedStress()
        {
            if(timedCoroutine!=null)
                StopCoroutine(timedCoroutine);
        }

        void ViewStress() => stressView.FillBar(stressController.Stress);
        
        void IncreaseByFailStress()
        {
            stressController.IncreaseStress(missStress);
            ViewStress();
        }

        void IncreaseByTimeStress()
        {
            stressController.IncreaseStress(timedStress);
            ViewStress();
        }

        void ReduceByHitStress()
        {
            stressController.IncreaseStress(-hitStress);
            ViewStress();
        }

        IEnumerator StressByTime()
        {
            while(true)
            {
                yield return new WaitForSeconds(1);
                IncreaseByTimeStress();   
            }
        }
    }
}