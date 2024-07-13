using System;
using UnityEngine;
using Voody.UniLeo;

namespace Assets._Project.Scripts.Components.GamePlay
{
    public sealed class FollowProvider : MonoProvider<FollowComponent> { }

    [Serializable]
    public struct FollowComponent
    {
        public Vector3 OffsetPosition;

        public Vector3 OffsetLookAt;

        public Transform Transform;
    }
}
