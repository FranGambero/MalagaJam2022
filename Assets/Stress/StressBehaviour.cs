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
                beatTrackController.onNoteOutOfSight.AddListener((NoteBehaviour)=>AddStress(10));
            }
            
            OnMaxStress.AddListener(()=>Debug.Log("Max Stress"));
        }

        void AddStress(int amount)
        {
            stressController.IncreaseStress(amount);
            ViewStress();
        }

        void ViewStress() => stressView.FillBar(stressController.Stress);
    }
}