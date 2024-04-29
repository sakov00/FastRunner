using Assets._Project.Scripts.Player.Models;

namespace Assets._Project.Scripts.Abilities.Abstracts
{
    public abstract class BaseAbility
    {
        protected PlayerModel _playerModel;
        public abstract void Activate();
    }
}
