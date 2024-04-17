using Assets.Scripts.ScriptableObjects;
using UnityEngine;

namespace Assets.Scripts.Camera
{
    public class CameraModel : MonoBehaviour
    {
        public CameraData cameraData;

        [field: SerializeField] public Transform Target { get; set; }

        public float SmoothValue { get; set; }

        public float DistanceFromGround { get; set; }

        public Vector3 Offset { get; set; }

        private void Awake()
        {
            SetDafaultState();
        }

        private void SetDafaultState()
        {
            SmoothValue = cameraData.SmoothValue;
            DistanceFromGround = cameraData.DistanceFromGround;
            Offset = cameraData.Offset;
        }
    }
}
