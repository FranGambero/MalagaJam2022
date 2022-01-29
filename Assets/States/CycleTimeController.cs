using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace MJam22.States
{
    public class CycleTimeController : MonoBehaviour
    {
        public UnityEvent OnEndOfCycle = new UnityEvent();
        
        int cycleTime;

        public void SetCycleTime(int seconds) => cycleTime = seconds;
        public void StartCycle() => StartCoroutine(Countdown(cycleTime));

        IEnumerator Countdown(int seconds)
        {
            var counter = seconds;
            while(counter > 0)
            {
                yield return new WaitForSeconds(1);
                counter--;
            }
            EndCycle();
        }

        void EndCycle() => OnEndOfCycle.Invoke();
    }
}