using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MJam22.BackgroundController
{
    public class BackgroundController : MonoBehaviour
    {
        [SerializeField] GameObject officeBackground;
        [SerializeField] GameObject clubBackground;
        [SerializeField] GameObject dragPosition;

        [SerializeField] Animator animator;
        [SerializeField] ParticleSystem officeHitParticle;

        [SerializeField] List<GameObject> dragPoses;
        private GameObject currenDragPose;

        [Header("Cookie Canvas")] 
        [SerializeField] Animator cookieCanvasAnimator;
        
        const string DRAG_IN = "DragIn";
        const string DRAG_OUT = "DragOut";

        bool isOffice;
        int currentCycle;

        public void OnNoteHitEffect()
        {
            if(isOffice)
            {
                //officeHitParticle.Play();
            }
            else
            {
                if(currenDragPose == null) {
                    currenDragPose = dragPoses.Find(p => p.activeSelf);
                }
                currenDragPose.SetActive(false);
                GameObject tmpCurrenDragPose = dragPoses[Random.Range(0, dragPoses.Count - 1)];
                if(tmpCurrenDragPose == currenDragPose) {
                    tmpCurrenDragPose = dragPoses[Random.Range(0, dragPoses.Count - 1)];
                }
                currenDragPose = tmpCurrenDragPose;
                currenDragPose.SetActive(true);
            }
        }

        public void OnNoteMissEffect()
        {
            if(isOffice)
            {
                
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
                if(currentCycle == 0)
                    LaunchTransitionAnimation(0, ChangeToOfficeMode, DRAG_OUT);
                else
                    ChangeToOfficeMode();
            }
            else
            {
                LaunchTransitionAnimation(0.5f, ChangeToDragMode, DRAG_IN);
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