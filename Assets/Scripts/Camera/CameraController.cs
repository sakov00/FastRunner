using UnityEngine;
using static Unity.Mathematics.math;

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
            var offset = _cameraModel.OffsetInAir;
            Vector3 rotationOffset = Quaternion.Euler(0, targetAngleY, 0) * offset;
            Vector3 newCameraPosition = _cameraModel.Target.position + rotationOffset;

            Ray ray = new Ray(newCameraPosition, Vector3.down);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && _characterController.isGrounded)
            {
                newCameraPosition.y = hit.point.y+1;
            }

            _cameraView.Move(Vector3.Lerp(transform.position, newCameraPosition, _cameraModel.SmoothValue * Time.deltaTime));
            _cameraView.LookAt(_cameraModel.Target);
        }
    }
}
