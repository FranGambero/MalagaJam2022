using System;
using System.Collections;
using System.Collections.Generic;
using MJam22.Beat;
using UnityEngine;

namespace MJam22.BackgroundController
{
    public class BackgroundController : MonoBehaviour
    {
        [SerializeField] GameObject officeBackground;
        [SerializeField] GameObject clubBackground;
        [SerializeField] GameObject dragPosition;

        [SerializeField] Animator officinistAnimator;
        [SerializeField] ParticleSystem officeHitParticle;

        [SerializeField] List<GameObject> dragPoses;
        [SerializeField] List<BeatTrackController> beatTrackControllers;
        private GameObject currenDragPose;

        [Header("Cookie Canvas")] 
        [SerializeField] Animator cookieCanvasAnimator;
        
        const string DRAG_IN = "dragIn";
        const string DRAG_OUT = "dragOut";
        const string DAMAGE_IN = "OnDamaged";

        bool isOffice;
        int currentCycle;

        void Start()
        {
            InitListeners();
        }

        void InitListeners()
        {
            foreach(var beatTrackController in beatTrackControllers)
            {
                beatTrackController.onNoteOutOfSight.AddListener((NoteBehaviour)=>OnNoteMissEffect());
                beatTrackController.onHitNote.AddListener(OnNoteHitEffect);
            }
        }

        public void OnNoteHitEffect()
        {
            if(isOffice)
            {
                officeHitParticle.Play();
            }
            else
            {
                if(currenDragPose == null) {
                    currenDragPose = dragPoses.Find(p => p.activeSelf);
                }
                currenDragPose.SetActive(false);
                GameObject tmpCurrenDragPose = dragPoses[UnityEngine.Random.Range(0, dragPoses.Count - 1)];
                if(tmpCurrenDragPose == currenDragPose) {
                    tmpCurrenDragPose = dragPoses[UnityEngine.Random.Range(0, dragPoses.Count - 1)];
                }
                currenDragPose = tmpCurrenDragPose;
                currenDragPose.SetActive(true);
            }
        }

        public void OnNoteMissEffect()
        {
            if(isOffice)
            {
                officinistAnimator?.SetTrigger(DAMAGE_IN);
            }
            else
            {
                
            }
        }

        public void ChangeStatus(bool isOffice, int cycle)
        {
            this.isOffice = isOffice;
            this.currentCycle = CalculatedDay(cycle);
            ChangeBackground();
        }

        int CalculatedDay(int cycle) {

            return (cycle % 2 == 0) ? cycle / 2 : (cycle - 1)/2;
        }

        void ChangeBackground()
        {
            if(isOffice)
            {
                if(currentCycle != 0)
                    LaunchTransitionAnimation(0.8f, ChangeToOfficeMode, DRAG_OUT);
                else
                    ChangeToOfficeMode();
            }
            else
            {
                LaunchTransitionAnimation(1f, ChangeToDragMode, DRAG_IN);
            }
        }

        void ChangeToOfficeMode()
        {
            officeBackground.SetActive(true);
            clubBackground.SetActive(false);
        }

        void ChangeToDragMode()
        {
            officeBackground.SetActive(false);
            clubBackground.SetActive(true);
        }

        void LaunchTransitionAnimation(float seconds, Action callback, string transitionMode)
        {
            StartCoroutine(ChangeBackgroundAfter(seconds, callback, transitionMode));
        }

        IEnumerator ChangeBackgroundAfter(float seconds, Action callback, string transitionMode)
        {
            cookieCanvasAnimator.SetTrigger(transitionMode);
            yield return new WaitForSeconds(seconds);
            callback?.Invoke();
        }
    }
}