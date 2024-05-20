using Assets._Project.Scripts.ScriptableObjects;

namespace Assets._Project.Scripts.Components.Player
{
    internal struct AbilityComponent
    {
        private float energyValue;
        public float EnergyValue
        {
            get { return energyValue; }
            set
            {
                if (value < 0)
                    energyValue = 0;
                else
                    energyValue = value;
            }
        }

        public bool AccelerationAbilityActivated;
        public bool DoubleJumpAbilityActivated;
        public bool EnergyShieldAbilityActivated;

        public PlayerData playerData;
    }
}
