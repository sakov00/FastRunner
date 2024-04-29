using Assets._Project.Scripts.ScriptableObjects.AbilitiesData.Abstract;
using System.Threading.Tasks;

namespace Assets._Project.Scripts.Abilities.Abstracts
{
    public abstract class ProlongedAbility : BaseAbility
    {
        protected bool isActive = false;
        protected static ProlongedAbilityData _prolongedAbilityData;

        protected override void OnLoad()
        {
            _playerModel.OnEnergyValueEnded += OnDeactivate;
        }

        public override void Activate()
        {
            if (!isActive)
            {
                isActive = true;
                OnActivate();
                Task.Run(() => Deactivate());
            }
        }

        public async Task Deactivate()
        {
            if (isActive)
            {
                await Task.Delay((int)(_prolongedAbilityData.EnergyTimer * 1000));
                isActive = false;
                OnDeactivate();
            }
        }

        protected abstract void OnActivate();

        protected abstract void OnDeactivate();
    }
}
