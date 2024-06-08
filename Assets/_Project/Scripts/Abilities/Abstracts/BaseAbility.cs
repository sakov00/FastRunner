using Assets._Project.Scripts.Player.Models;
using Assets._Project.Scripts.ScriptableObjects.AbilitiesData.Abstract;

namespace Assets._Project.Scripts.Abilities.Abstracts
{
    public abstract class BaseAbility
    {
        protected PlayerModel _playerModel;
        public AbilityData AbilityData { get; protected set; }

        public abstract void Activate();
    }
}
