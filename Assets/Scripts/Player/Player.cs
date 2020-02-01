using System;
using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour
    {
        public float JumpForce = 400;
        public float MoveSpeed = 40;

        public PlayerControl Control;

        private void OnEnable()
        {
            Control.OnHoldCancel += HoldCancelHandler;
            Control.OnHoldStart += HoldStartHandler;
            Control.OnHoldFinish += HoldFinishHandler;
        }
        
        void OnDisable()
        {
            Control.OnHoldCancel -= HoldCancelHandler;
            Control.OnHoldStart -= HoldStartHandler;
            Control.OnHoldFinish -= HoldFinishHandler;
        }

        private void HoldCancelHandler()
        {
            Debug.Log("hold cancel");
        }

        private void HoldStartHandler(bool value)
        {
            Debug.Log($"hold {(value ? "increase": "decrease")} start");
        }

        private void HoldFinishHandler(bool value)
        {
            Debug.Log($"hold {(value ? "increase": "decrease")} success finish");
        }
    }
}
