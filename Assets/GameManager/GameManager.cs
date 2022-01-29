using MJam22.States;
using UnityEngine;

namespace MJam22.GameManager
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] StateController stateController;

        public void Start()
        {
            stateController.Init();
        }

        public void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space))
                stateController.LaunchCurrentState();
        }
    }
}