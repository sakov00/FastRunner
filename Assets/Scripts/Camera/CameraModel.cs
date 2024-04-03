using Assets.Scripts.ScriptableObjects;
using UnityEngine;

namespace Assets.Scripts.Camera
{
    public class CameraModel : MonoBehaviour
    {
        public CameraData cameraData;

        [field: SerializeField] public Transform Target { get; set; }

        public float SmoothSpeed { get; set; }

        public Vector3 OffsetInAir { get; set; }

        public Vector3 OffsetInGround { get; set; }

        private void Awake()
        {
            SmoothSpeed = cameraData.SmoothSpeed;
            OffsetInAir = cameraData.OffsetInAir;
            OffsetInGround = cameraData.OffsetInGround;
        }
    }
}
