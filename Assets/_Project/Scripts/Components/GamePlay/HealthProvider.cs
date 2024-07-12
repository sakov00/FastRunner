using System;
using UnityEngine;
using Voody.UniLeo;

namespace Assets._Project.Scripts.Components.GamePlay
{
    public sealed class HealthProvider : MonoProvider<HealthComponent> { }

    [Serializable]
    public struct HealthComponent
    {
        [SerializeField] private float healthPoints;
        public float HealthPoints
        {
            get { return healthPoints; }
            set
            {
                if (value < HealthPointsMin)
                    healthPoints = HealthPointsMin;
                if (value > HealthPointsMax)
                    healthPoints = HealthPointsMax;
                if (value >= HealthPointsMin && value <= HealthPointsMax)
                    healthPoints = value;
            }
        }

        public float HealthPointsMin;
        public float HealthPointsMax;
        [NonSerialized] public float CurrentDamageCoolDown;
        public float DamageCoolDown;
    }
}
