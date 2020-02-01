using Player;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ProgressBarManger : MonoBehaviour
    {

        public Transform PlayerTransform;
        public Camera Camera;
        public PlayerControl Control;

        public GameObject ProgressBar;
        public RectTransform ProgressBarTransform;
        public Slider Slider;

        private void OnEnable()
        {
            Control.OnHoldCancel += HoldCancelHandler;
            Control.OnHoldStart += HoldStartHandler;
            Control.OnHoldProgress += HoldProgressHandler;
            Control.OnHoldFinish += HoldFinishHandler;
        }
        
        void OnDisable()
        {
            Control.OnHoldCancel -= HoldCancelHandler;
            Control.OnHoldStart -= HoldStartHandler;
            Control.OnHoldProgress -= HoldProgressHandler;
            Control.OnHoldFinish -= HoldFinishHandler;
        }
    
    
        private void HoldCancelHandler()
        {
            ProgressBar.SetActive(false);
        }

        private void HoldStartHandler(bool value)
        {
            var position = Camera.WorldToScreenPoint(PlayerTransform.position) + Vector3.up * 50;
            ProgressBarTransform.position = position;
            ProgressBar.SetActive(true);
        }

        private void HoldFinishHandler(bool value)
        {
            ProgressBar.SetActive(false);
        }

        private void HoldProgressHandler(bool value, float progress)
        {
            Slider.value = progress;
        }
    }
}
