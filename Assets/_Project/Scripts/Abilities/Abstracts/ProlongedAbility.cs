using Assets._Project.Scripts.Player.Models;
using Assets._Project.Scripts.ScriptableObjects.AbilitiesData.Abstract;
using System.Threading.Tasks;
using Zenject;

namespace Assets._Project.Scripts.Abilities.Abstracts
{
    public abstract class ProlongedAbility : BaseAbility
    {
        protected bool isActive = false;

        protected ProlongedAbilityData ProlongedAbilityData
        {
            get { return (ProlongedAbilityData)AbilityData; }
            set { AbilityData = value; }
        }

        protected ProlongedAbility(PlayerModel playerModel)
        {
            _playerModel = playerModel;

            _playerModel.OnEnergyValueEnded += Deactivate;
        }

        public void FixedTick()
        {
            if (isActive)
                _playerModel.EnergyValue -= ProlongedAbilityData.EnergyPerSecond / 50;
        }

        public override void Activate()
        {
            if (!isActive && _playerModel.EnergyValue > 0)
            {
                isActive = true;
                OnActivate();
                Task.Run(async () =>
                {
                    await Task.Delay((int)(ProlongedAbilityData.EnergyTimer * 1000));
                    Deactivate();
                });
            }
        }

        public void Deactivate()
        {
            if (isActive)
            {
                isActive = false;
                OnDeactivate();
            }
        }

        protected abstract void OnActivate();

        protected abstract void OnDeactivate();
    }
}
