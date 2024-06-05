using Assets._Project.Scripts.ScriptableObjects;

namespace Assets._Project.Scripts.Components.Player
{
    internal struct AbilityComponent
    {
        private float energyPoints;
        public float EnergyPoints
        {
            get { return energyPoints; }
            set
            {
                if (value < EnergyPointsMin)
                    energyPoints = EnergyPointsMin;
                if (value > EnergyPointsMax)
                    energyPoints = EnergyPointsMax;
                else
                    energyPoints = value;
            }
        }

        public float EnergyPointsMin { get; set; }
        public float EnergyPointsMax { get; set; }

        public bool AccelerationAbilityActivated;
        public bool DoubleJumpAbilityActivated;
        public bool EnergyShieldAbilityActivated;

        public PlayerData playerData;
    }
}
