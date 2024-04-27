using Assets.Scripts.Player.Models;
using UnityEngine;

namespace Assets.Scripts.Abilities
{
    public class AccelerationAbility : BaseAbility
    {
        private readonly PlayerModel _playerModel;
        private readonly CharacterController _characterController;

        public AccelerationAbility(PlayerModel playerModel, CharacterController characterController)
        {
            _playerModel = playerModel;
            _characterController = characterController;
        }

        public override void StartAbility()
        {
            _playerModel.RunningSpeed = 30;
        }
    }
}
