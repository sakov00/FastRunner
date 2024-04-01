using UnityEngine;

namespace Assets.Scripts.Camera
{
    public class CameraController : MonoBehaviour
    {
        private CameraModel _cameraModel;
        private CameraView _cameraView;

        private void Awake()
        {
            _cameraModel = GetComponent<CameraModel>();
            _cameraView = GetComponent<CameraView>();
        }

        void LateUpdate()
        {
            _cameraView.Move(Vector3.Lerp(transform.position, _cameraModel.Target.position + _cameraModel.Offset, _cameraModel.SmoothSpeed));
            _cameraView.Rotate(_cameraModel.Target.rotation);
        }
    }
}
