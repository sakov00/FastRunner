using UnityEngine;

namespace Assets.Scripts.Camera
{
    public class CameraController : MonoBehaviour
    {
        private CameraModel _cameraModel;
        private CameraView _cameraView;

        private CharacterController _characterController;

        private void Awake()
        {
            _cameraModel = GetComponent<CameraModel>();
            _cameraView = GetComponent<CameraView>();
            _characterController = _cameraModel.Target.GetComponent<CharacterController>();
        }

        private void LateUpdate()
        {
            var targetAngleY = _cameraModel.Target.eulerAngles.y;
            var offset = _characterController.isGrounded ? _cameraModel.OffsetInGround : _cameraModel.OffsetInAir;
            Vector3 rotationOffset = Quaternion.Euler(0, targetAngleY, 0) * offset;
            Vector3 newCameraPosition = _cameraModel.Target.position + rotationOffset;

            _cameraView.Move(Vector3.Lerp(transform.position, newCameraPosition, _cameraModel.SmoothSpeed));
            _cameraView.LookAt(_cameraModel.Target);
        }
    }
}
