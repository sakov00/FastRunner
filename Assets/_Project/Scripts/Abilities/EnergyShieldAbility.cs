using Assets._Project.Scripts.Abilities.Abstracts;
using Assets._Project.Scripts.Player.Models;
using Assets._Project.Scripts.ScriptableObjects.AbilitiesData;

namespace Assets._Project.Scripts.Abilities
{
    public class EnergyShieldAbility : ProlongedAbility
    {
        protected static EnergyShieldAbilityData AbilityData
        {
            get { return (EnergyShieldAbilityData)_prolongedAbilityData; }
            set { _prolongedAbilityData = value; }
        }

        public EnergyShieldAbility(PlayerModel playerModel)
        {
            _playerModel = playerModel;

            OnLoad();
        }

        public static void InjectData(EnergyShieldAbilityData energyShieldAbilityData)
        {
            AbilityData = energyShieldAbilityData;
        }

        protected override void OnActivate()
        {

        }

        protected override void OnDeactivate()
        {

        }
    }
}
