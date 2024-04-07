using Assets.Scripts.ScriptableObjects;
using UnityEngine;

namespace Assets.Scripts.Camera
{
    public class CameraModel : MonoBehaviour
    {
        public CameraData cameraData;

        [field: SerializeField] public Transform Target { get; set; }

        public float SmoothValue { get; set; }

        public float LookingSpeed { get; set; }

        public Vector3 OffsetInAir { get; set; }

        public Vector3 OffsetInGround { get; set; }

        private void Awake()
        {
            SmoothValue = cameraData.SmoothValue;
            LookingSpeed = cameraData.LookingSpeed;
            OffsetInAir = cameraData.OffsetInAir;
            OffsetInGround = cameraData.OffsetInGround;
        }
    }
}
