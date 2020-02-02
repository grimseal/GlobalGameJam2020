using UnityEngine;

namespace InteractiveObject
{
    public class ObjectArrow : MonoBehaviour
    {
        public float MinValueAngle;

        public float MaxValueAngle;

        private Transform arrowTransform;
        
        private float val;
        private float NormalizedValue
        {
            get => val;
            set
            {
                val = value;
                var angle = Mathf.Lerp(MinValueAngle, MaxValueAngle, val);
                arrowTransform.rotation = Quaternion.Euler(0, 0, angle);
            }
        }

        private void Awake()
        {
            arrowTransform = transform;
        }

        public void SetValue(float value)
        {
            NormalizedValue = 1f / 100f * value;
        }

    }
}
