using Assets._Project.Scripts.ScriptableObjects;
using UnityEngine;
using Zenject;

namespace Assets._Project.Scripts.Camera
{
    public class CameraController : MonoBehaviour
    {
        private CameraData _cameraData;
        private CameraView _cameraView;
        private CharacterController _characterController;

        [field: SerializeField] private Transform Target { get; set; }

        [Inject]
        private void Contract(CameraData cameraData)
        {
            _cameraData = cameraData;
        }

        private void Awake()
        {
            _cameraView = GetComponent<CameraView>();
            _characterController = Target.GetComponent<CharacterController>();
        }

        private void LateUpdate()
        {
            var targetAngleY = Target.eulerAngles.y;
            var offset = _cameraData.Offset;
            Vector3 rotationOffset = Quaternion.Euler(0, targetAngleY, 0) * offset;
            Vector3 newCameraPosition = Target.position + rotationOffset;

            Ray ray = new Ray(newCameraPosition, Vector3.down);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && _characterController.isGrounded)
            {
                newCameraPosition.y = hit.point.y + _cameraData.DistanceFromGround;
            }

            _cameraView.Move(Vector3.Lerp(transform.position, newCameraPosition, _cameraData.SmoothValue * Time.deltaTime));
            _cameraView.LookAt(Target);
        }
    }
}
