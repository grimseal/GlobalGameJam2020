using UnityEngine;

namespace Environment
{
    public class ScenePitching : MonoBehaviour
    {
        public Transform CameraTransform;
        public Transform ChainContainer;

        public float OffsetMultiplyer = 0.5f;
        public float AngleMultiplyer = 2f;
        public float ChainAngleMultiplyer = 4f;
        public float ChainTimeOffset = 1.2f;

        void Update()
        {
            var sin = Mathf.Sin(Time.time);
            var angle = sin * AngleMultiplyer;
            CameraTransform.rotation = Quaternion.Euler(0, 0, angle);

            var offset = sin * OffsetMultiplyer;
            CameraTransform.position = new Vector3(0, offset, -10);

            var chainAngle = Mathf.Sin(Time.time - ChainTimeOffset) * -ChainAngleMultiplyer; 
            ChainContainer.rotation = Quaternion.Euler(0, 0, chainAngle);
        }
    }
}