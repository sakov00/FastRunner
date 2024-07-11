using System;
using UnityEngine;
using Voody.UniLeo;

namespace Assets._Project.Scripts.Components
{
    public sealed class FollowProvider : MonoProvider<FollowComponent> { }

    [Serializable]
    public struct FollowComponent
    {
        public Vector3 Offset;

        public Transform Transform;
    }
}
