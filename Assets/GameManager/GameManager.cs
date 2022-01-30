using System;
using MJam22.States;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MJam22.GameManager
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] StateController stateController;
        int currentCycle = 0;

        void Start()
        {
            FirstLaunch();
        }

        public void NextCycle()
        {
            currentCycle++;
            if(currentCycle >= 6)
            {
                NoMoreCycles();
                return;
            }
                
            
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

        void NoMoreCycles()
        {
            SceneManager.LoadScene(2);
        }
    }
}