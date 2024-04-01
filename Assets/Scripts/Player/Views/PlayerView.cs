using UnityEngine;

namespace Assets.Scripts.Player.Views
{
    public class PlayerView : MonoBehaviour
    {
        private CharacterController _characterController;

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        public void Move(Vector3 movement)
        {
            _characterController.Move(movement);
        }

        public void Rotate(Quaternion quaternion)
        {
            transform.rotation = quaternion;
        }
    }
}
