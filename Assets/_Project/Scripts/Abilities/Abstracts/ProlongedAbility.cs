
using Assets._Project.Scripts.ScriptableObjects.AbilitiesData;

namespace Assets._Project.Scripts.Abilities.Abstracts
{
    public abstract class ProlongedAbility : BaseAbility
    {
        protected bool isActive = false;
        protected ProlongedAbilityData _prolongedAbilityData;

        public override void Activate()
        {
            if (!isActive)
            {
                isActive = true;
                OnActivate();
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

        protected abstract bool OnActivate();

        protected abstract bool OnDeactivate();
    }
}
