using System;
using UnityEngine;

namespace Assets._Project.Scripts.Components.Object
{
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
                else
                    healthPoints = value;
            }
        }

        public float HealthPointsMin;
        public float HealthPointsMax;
        [NonSerialized] public float CurrentDamageCoolDown;
        public float DamageCoolDown;
    }
}
