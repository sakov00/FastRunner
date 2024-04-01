using System;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Camera
{
    public class CameraModel : MonoBehaviour
    {
        [field: SerializeField] public Transform Target { get; set; }

        [field: SerializeField] public float SmoothSpeed { get; set; }

        [field: SerializeField] public Vector3 Offset { get; set; }
    }
}
