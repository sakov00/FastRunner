using UnityEngine;

namespace Assets.Scripts.Player.Models
{
    public class PlayerModel : MonoBehaviour
    {
        [field: SerializeField] public float RunSpeed { get; set; }

        [field: SerializeField] public float JumpHeight { get; set; }

        [field: SerializeField] public float GravityValue { get; set; }
    }
}
