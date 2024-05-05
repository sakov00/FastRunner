using Assets._Project.Scripts.Player.Views;
using UnityEngine;
using Zenject;

namespace Assets._Project.Scripts.Player.Controllers
{
    public class PlayerMovementController : MonoBehaviour
    {
        private PlayerView _playerView;
        private CharacterController _characterController;

        private GroundMovement _groundMovement;
        private AirMovement _airMovement;

        private Vector3 _movement = Vector3.zero;
        private Quaternion _rotation;

        [Inject]
        private void Contract(GroundMovement groundMovement, AirMovement airMovement, PlayerView playerView, CharacterController characterController)
        {
            _groundMovement = groundMovement;
            _airMovement = airMovement;

            _playerView = playerView;
            _characterController = characterController;
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
