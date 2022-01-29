using UnityEngine;
using UnityEngine.UI;

namespace MJam22.Stress
{
    public class StressView : MonoBehaviour
    {
        [SerializeField] Image fillBar;

        public void FillBar(float amount)
        {
            fillBar.fillAmount = amount / 100;
        }
    }
}