using System;
using System.Collections;
using InteractiveObject;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerControl : MonoBehaviour
    {
        public Player Player;
        public CharacterController2D controller;
        public SpriteRenderer SpriteRenderer;
        public Animator Animator;
    
        public float ValueChangeTime = 2f;

        public event Action<bool> OnHoldStart;
        public event Action<bool, float> OnHoldProgress;
        public event Action<bool> OnHoldFinish;
        public event Action OnHoldCancel;

        private bool hold;
        private bool changed;
        float horizontalMove;
        bool jump;
        private bool enteredInteractableObject;

        private IEnumerator holdCorutine;

        private static readonly int AnimatorMove = Animator.StringToHash("move");
        private static readonly int AnimatorUse = Animator.StringToHash("use");

        private ChildController currentChildController;
        private GrapplingHookController grapplingHookController;
        
        
        // public 

        public void OnJump(InputAction.CallbackContext context)
        {
            if (hold) return;
            jump = true;
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Started:
                    if (hold) return;
                    horizontalMove = context.ReadValue<float>() * Player.MoveSpeed;
                    Animator.SetBool(AnimatorMove, true);
                    SpriteRenderer.flipX = horizontalMove < 0f;
                    break;
                case InputActionPhase.Canceled:
                    horizontalMove = context.ReadValue<float>() * Player.MoveSpeed;
                    Animator.SetBool(AnimatorMove, false);
                    break;
            }
        }

        public void OnUse(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        public void OnIncrease(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Started:
                    if (!enteredInteractableObject) break;
                    hold = true;
                    StopHoldCoroutine();
                    holdCorutine = HoldCoroutine(true);
                    StartCoroutine(holdCorutine);
                    horizontalMove = 0;
                    Animator.SetBool(AnimatorMove, false);
                    break;
                case InputActionPhase.Canceled:
                    hold = false;
                    StopHoldCoroutine();
                    break;
            }
        }
    
        public void OnDecrease(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Started:
                    if (!enteredInteractableObject) break;
                    hold = true;
                    StopHoldCoroutine();
                    holdCorutine = HoldCoroutine(false);
                    StartCoroutine(holdCorutine);
                    horizontalMove = 0;
                    Animator.SetBool(AnimatorMove, false);
                    break;
                case InputActionPhase.Canceled:
                    hold = false;
                    StopHoldCoroutine();
                    break;
            }
        }

        private void StopHoldCoroutine()
        {
            if (holdCorutine == null) return;
            if (OnHoldCancel != null) OnHoldCancel();
            StopCoroutine(holdCorutine);
            holdCorutine = null;
        }

        private IEnumerator HoldCoroutine(bool increase)
        {
            if (OnHoldStart != null) OnHoldStart(increase);
            var startTime = Time.time;
            var endTime = Time.time + ValueChangeTime;
            while (Time.time < endTime)
            {
                if (OnHoldProgress != null) 
                    OnHoldProgress(increase, 1f / ValueChangeTime * (Time.time - startTime));
                yield return null;
            }
            holdCorutine = null;
            if (OnHoldFinish != null)
                OnHoldFinish(increase);
            TriggerInteractiveObjectAction(increase);
        }

        void FixedUpdate ()
        {
            controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
            jump = false;
        }


        private void TriggerInteractiveObjectAction(bool increment)
        {
            if (!enteredInteractableObject) return;

            if (currentChildController != null)
            {
                var value = currentChildController.partIncrementValue * (increment ? 1 : -1);
                currentChildController.childAction.ChangeProblemValue(value);
            }
        }
        
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            enteredInteractableObject = true;
            currentChildController = other.GetComponent<ChildController>();
            grapplingHookController = other.GetComponent<GrapplingHookController>();
            if (grapplingHookController != null) grapplingHookController.Active = true;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            enteredInteractableObject = false;
            currentChildController = null;
            if (grapplingHookController != null)
            {
                grapplingHookController.Active = false;
                grapplingHookController = null;
            }
        }
    }
}