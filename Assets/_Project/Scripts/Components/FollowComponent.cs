using Assets._Project.Scripts.Components.Unit;
using UnityEngine;

namespace Assets._Project.Scripts.Components
{
    internal struct FollowComponent
    {
        public Vector3 Offset;

        public UnitMovementComponent TargetMovementComponent;
    }
}
