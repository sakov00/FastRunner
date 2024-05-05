using Assets._Project.Scripts.Abilities.Abstracts;
using Assets._Project.Scripts.Player.Models;
using Assets._Project.Scripts.ScriptableObjects.AbilitiesData;

namespace Assets._Project.Scripts.Abilities
{
    public class AccelerationAbility : ProlongedAbility
    {
        protected AccelerationAbilityData AccelerationAbilityData
        {
            get { return (AccelerationAbilityData)ProlongedAbilityData; }
            set { ProlongedAbilityData = value; }
        }

        public AccelerationAbility(PlayerModel playerModel, AccelerationAbilityData accelerationAbilityData) : base(playerModel)
        {
            AccelerationAbilityData = accelerationAbilityData;
        }

        protected override void OnActivate()
        {
            _playerModel.RunningSpeed += AccelerationAbilityData.ValueSpeedUp;
        }

        protected override void OnDeactivate()
        {
            _playerModel.RunningSpeed -= AccelerationAbilityData.ValueSpeedUp;
        }
    }
}
