using UnityEngine;

namespace MJam22.States
{
    [CreateAssetMenu(fileName = "StateData", menuName = "ScriptableObjects/StateData/NewData")]
    public class StateDataModelScriptable : ScriptableObject
    {
        [SerializeField] StateDataModel data;

        public StateDataModel Data => data;
    }
}