using Assets._Project.InputSystem;
using Assets._Project.Scripts.Player.Models;
using Assets._Project.Scripts.Player.Views;
using Assets._Project.Scripts.ScriptableObjects;
using UnityEngine;
using Zenject;

namespace Assets._Project.Scripts.Player.Controllers
{
    public class PlayerMovementController : MonoBehaviour
    {
        private PlayerModel _playerModel;
        private PlayerData _playerData;
        private IPlayerInput _playerInput;
        private PlayerView _playerView;
        private CharacterController _characterController;

        private GroundMovement _groundMovement;
        private AirMovement _airMovement;

        private Vector3 _movement = Vector3.zero;
        private Quaternion _rotation;

        [Inject]
        private void Contract(PlayerData playerData, IPlayerInput playerInput)
        {
            _playerData = playerData;
            _playerInput = playerInput;
        }

        private void Awake()
        {
            _playerModel = GetComponent<PlayerModel>();
            _playerView = GetComponent<PlayerView>();
            _characterController = GetComponent<CharacterController>();

            _groundMovement = new GroundMovement(_playerModel, _playerData, _playerInput);
            _airMovement = new AirMovement(_playerModel, _playerData, _playerInput);
        }

        private void Update()
        {
            if (_characterController.isGrounded)
            {
                _movement = _groundMovement.Move(_movement);
                _rotation = _groundMovement.Rotate(_rotation);
            }
            else
            {
                _movement = _airMovement.Move(_movement);
                _rotation = _airMovement.Rotate(_rotation);
            }

            _playerView.Move(_movement * Time.deltaTime);
            _playerView.Rotate(_rotation);
        }

        public void SetMovement(Vector3 movement)
        {
            _movement = movement;
        }
    }
}
