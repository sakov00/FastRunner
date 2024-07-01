using System;
using UnityEngine;

namespace Assets._Project.Scripts.Components
{
    [Serializable]
    public struct FollowComponent
    {
        public Vector3 Offset;

        public Transform Transform;
    }
}
