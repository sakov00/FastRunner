using Assets._Project.Scripts.Enums;
using System;
using UnityEngine;
using Voody.UniLeo;

namespace Assets._Project.Scripts.Components.GamePlay
{
    public class SpawnerProvider : MonoProvider<SpawnerComponent> { }

    [Serializable]
    public struct SpawnerComponent
    {
        public SpawnEffectType Effect;
        public SpawnObjectType Object;

        public bool IsActivated;

        public Transform PointSpawn;
        public float InnerRadiusSpawn;
        public float OuterRadiusSpawn;

        public float Height;
        public float MinAngle;
        public float MaxAngle;

        public float CoolDown;
        public float CurrentTime;
    }
}
