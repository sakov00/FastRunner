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

        //Enable when someone want fast test parameters and set the best values
        private void FixedUpdate()
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
