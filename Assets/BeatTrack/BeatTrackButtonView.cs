using UnityEngine;
using UnityEngine.UI;

namespace MJam22.Beat
{
    public class BeatTrackButtonView : MonoBehaviour
    {
        [SerializeField] GameObject UnPressedBtn;
        [SerializeField] GameObject PressedBtn;

        public void Init() => Pressed(true);
        public void Press() => Pressed(true);
        public void UnPress() => Pressed(false);

        void Pressed(bool isPressed)
        {
            PressedBtn.SetActive(isPressed);
            UnPressedBtn.SetActive(!isPressed);
        }
    }
}