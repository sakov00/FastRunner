using Assets._Project.Scripts.Player.Models;

namespace Assets._Project.Scripts.Abilities.Abstracts
{
    public abstract class BaseAbility
    {
        protected PlayerModel _playerModel;
        protected abstract void OnLoad();
        public abstract void Activate();
    }
}
