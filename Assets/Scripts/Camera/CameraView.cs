using System;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Camera
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
    }
}
