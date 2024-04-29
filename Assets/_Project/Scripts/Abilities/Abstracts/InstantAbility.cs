using Assets._Project.Scripts.ScriptableObjects.AbilitiesData.Abstract;

namespace Assets._Project.Scripts.Abilities.Abstracts
{
    public abstract class InstantAbility : BaseAbility
    {
        protected bool isCompleted = false;
        protected static InstantAbilityData _instantAbilityData;

        protected override void OnLoad()
        {
        }

        public override void Activate()
        {
            if (_playerModel.EnergyValue > 0)
            {
                OnActivate();
                if (isCompleted)
                {
                    _playerModel.EnergyValue -= _instantAbilityData.EnergyCost;
                    isCompleted = false;
                }
            }
        }

        protected abstract void OnActivate();
    }
}
