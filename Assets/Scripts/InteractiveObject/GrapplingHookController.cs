using System;
using System.Collections;
using Helpers;
using UnityEngine;

namespace InteractiveObject
{
    public class GrapplingHookController : MonoBehaviour
    {
        public SpriteRenderer ChainSprite;
        public Transform HookTransform;
        public Collider2D HookCollider;
        public Player.PlayerControl Control;

        public float ChainMaxLength = 20f;
        public float ChainMinLength = 1f;
        public float AnimationSpeed = 0.5f;

        public bool CanGrab;
        public bool Active;

        public static GrapplingHookController Instance;
    
        public float ChainLength
        {
            set
            {
                ChainSprite.size = new Vector2(value, ChainSprite.size.y);
                HookTransform.localPosition = new Vector3(0, -value, 0);
            }
            get => ChainSprite.size.x;
        }

        public bool IsDropped = false;
        private Coroutine resetCoroutine;

        private void Awake()
        {
            Instance = this;
            ChainLength = IsDropped ? ChainMaxLength : ChainMinLength;
        }

        public IEnumerator ResetLengthCoroutine(float time)
        {
            var startTime = Time.time;
            var finishTime = startTime + time;
            var startLength = ChainLength;
            var targetLength = IsDropped ? ChainMaxLength : ChainMinLength;

            while (Time.time < finishTime)
            {
                ChainLength = EasingFunction.EaseOutBounce(startLength, targetLength, 1f / time * (Time.time - startTime));
                yield return null;
            }

            ChainLength = targetLength;
            resetCoroutine = null;
        }

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
            if (!Active) return;
            resetCoroutine = StartCoroutine(ResetLengthCoroutine(AnimationSpeed));
        }

        private void HoldStartHandler(bool value)
        {
            if (!Active) return;
            if (resetCoroutine != null) StopCoroutine(resetCoroutine);
            resetCoroutine = null;
        }

        private void HoldFinishHandler(bool value)
        {
            if (!Active) return;
            IsDropped = !value;
             if (CanGrab) ResourceManager.Instance.AddResources();
        }

        private void HoldProgressHandler(bool up, float progress)
        {
            if (!Active) return;
            var startLength = IsDropped ? ChainMaxLength : ChainMinLength;
            var targetLength = up ? ChainMinLength : ChainMaxLength;
            ChainLength = Mathf.Lerp(startLength, targetLength, progress);
        }

    }
}
