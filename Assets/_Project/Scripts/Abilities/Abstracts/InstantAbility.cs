using Assets._Project.Scripts.Player.Models;
using Assets._Project.Scripts.ScriptableObjects.AbilitiesData;

namespace Assets._Project.Scripts.Abilities.Abstracts
{
    public abstract class InstantAbility : BaseAbility
    {
        protected InstantAbilityData _instantAbilityData;
        public override void Activate()
        {
            if (_playerModel.EnergyValue > _instantAbilityData.EnergyCost && ExecuteAbility())
                _playerModel.EnergyValue -= _instantAbilityData.EnergyCost;
        }

        protected abstract bool ExecuteAbility();
    }
}
