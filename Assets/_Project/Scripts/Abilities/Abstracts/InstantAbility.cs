using Assets._Project.Scripts.Player.Models;
using Assets._Project.Scripts.ScriptableObjects.AbilitiesData.Abstract;

namespace Assets._Project.Scripts.Abilities.Abstracts
{
    public abstract class InstantAbility : BaseAbility
    {
        protected bool isCompleted = false;

        protected InstantAbilityData ProlongedAbilityData
        {
            get { return (InstantAbilityData)AbilityData; }
            set { AbilityData = value; }
        }

        protected InstantAbility(PlayerModel playerModel)
        {
            _playerModel = playerModel;
        }

        public override void Activate()
        {
            if (_playerModel.EnergyValue > 0)
            {
                OnActivate();
                if (isCompleted)
                {
                    _playerModel.EnergyValue -= ProlongedAbilityData.EnergyCost;
                    isCompleted = false;
                }
            }
        }

        protected abstract void OnActivate();
    }
}
