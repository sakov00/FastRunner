using System;
using UnityEngine;
using Voody.UniLeo;

namespace Assets._Project.Scripts.Components.GamePlay
{
    public class SpawnerProvider : MonoProvider<SpawnerComponent> { }

    [Serializable]
    public struct SpawnerComponent
    {
        public UnityEngine.Object EffectPrefab;
        public UnityEngine.Object Prefab;

        public bool IsActive;

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
