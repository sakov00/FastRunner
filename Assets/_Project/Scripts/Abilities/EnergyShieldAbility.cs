using Assets._Project.Scripts.Abilities.Abstracts;
using Assets._Project.Scripts.Player.Models;
using Assets._Project.Scripts.ScriptableObjects.AbilitiesData;

namespace Assets._Project.Scripts.Abilities
{
    public class EnergyShieldAbility : ProlongedAbility
    {
        protected EnergyShieldAbilityData EnergyShieldAbilityData
        {
            get { return (EnergyShieldAbilityData)ProlongedAbilityData; }
            set { ProlongedAbilityData = value; }
        }

        public EnergyShieldAbility(PlayerModel playerModel, EnergyShieldAbilityData energyShieldAbilityData) : base(playerModel)
        {
            EnergyShieldAbilityData = energyShieldAbilityData;
        }

        protected override void OnActivate()
        {

        }

        protected override void OnDeactivate()
        {

        }
    }
}
