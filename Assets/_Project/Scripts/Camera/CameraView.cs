using UnityEngine;

namespace Assets._Project.Scripts.Camera
{
    public class CameraView : MonoBehaviour
    {
        public void Move(Vector3 movement)
        {
            transform.position = movement;
        }

        public void Rotate(Quaternion quaternion)
        {
            transform.rotation = quaternion;
        }

        public void LookAt(Transform target)
        {
            transform.LookAt(target);
        }
    }
}
