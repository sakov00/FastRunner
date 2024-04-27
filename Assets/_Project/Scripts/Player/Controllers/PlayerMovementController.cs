using Assets.Scripts.Player.Models;
using Assets.Scripts.Player.Views;
using Assets.Scripts.ScriptableObjects;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Player.Controllers
{
    public class PlayerMovementController : MonoBehaviour
    {
        private PlayerModel _playerModel;
        private PlayerData _playerData;
        private PlayerInputController _playerInputController;
        private PlayerView _playerView;
        private CharacterController _characterController;

        private GroundMovement groundMovement;
        private AirMovement airMovement;

        private Vector3 _movement = Vector3.zero;
        private Quaternion _rotation;

        [Inject]
        private void Contract(PlayerData playerData)
        {
            _playerData = playerData;
        }

        private void Awake()
        {
            _playerModel = GetComponent<PlayerModel>();
            _playerInputController = GetComponent<PlayerInputController>();
            _playerView = GetComponent<PlayerView>();
            _characterController = GetComponent<CharacterController>();
        }

        private void Start()
        {
            groundMovement = new GroundMovement(_playerModel, _playerData, _playerInputController);
            airMovement = new AirMovement(_playerModel, _playerData, _playerInputController);
        }

        private void Update()
        {
            if (_characterController.isGrounded)
            {
                _movement = groundMovement.Move(_movement);
                _rotation = groundMovement.Rotate(_rotation);
            }
            else
            {
                _movement = airMovement.Move(_movement);
                _rotation = airMovement.Rotate(_rotation);
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
