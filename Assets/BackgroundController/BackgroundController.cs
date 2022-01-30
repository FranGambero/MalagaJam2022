using System.Collections.Generic;
using UnityEngine;

namespace MJam22.BackgroundController
{
    public class BackgroundController : MonoBehaviour
    {
        [SerializeField] GameObject officeBackground;
        [SerializeField] GameObject clubBackground;

        [SerializeField] Animator animator;
        [SerializeField] ParticleSystem officeHitParticle;

        [SerializeField] List<GameObject> dragPoses;

        bool isOffice;
        int currentCycle;

        public void OnNoteHitEffect()
        {
            if(isOffice)
            {
                
            }
            else
            {
                
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
                officeBackground.SetActive(true);
                clubBackground.SetActive(false);
            }
            else
            {
                officeBackground.SetActive(false);
                clubBackground.SetActive(true);
            }
        }
    }
}