using MJam22.States;
using UnityEngine;

namespace MJam22.GameManager
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] StateController stateController;
        int currentCycle = 0;

        public void Start()
        {
            stateController.Init();
        }

        public void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space))
                FirstLaunch();
        }
        
        public void NextCycle()
        {
            currentCycle++;
            if(currentCycle >= 8)
                return;
            
            LoadCycle();
            LaunchCycle();
        }

        void FirstLaunch()
        {
            LoadCycle();
            LaunchCycle();
        }

        void LoadCycle()
        {
            stateController.LoadState(currentCycle);
        }

        void LaunchCycle() => stateController.LaunchCurrentState();
    }
}