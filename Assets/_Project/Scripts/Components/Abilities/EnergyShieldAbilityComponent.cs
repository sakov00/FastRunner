using Assets._Project.Scripts.Components.Abilities.Interfaces;
using Assets._Project.Scripts.Enums;
using Assets._Project.Scripts.ScriptableObjects.AbilitiesData;

namespace Assets._Project.Scripts.Components.Abilities
{
    public struct EnergyShieldAbilityComponent : IProlongedAbilityComponent
    {
        public AbilityType AbilityType { get; set; }
        public float CurrentWorkTime { get; set; }
        public bool IsActive { get; set; }
        public EnergyShieldAbilityData EnergyShieldAbilityData { get; set; }

    }
}
