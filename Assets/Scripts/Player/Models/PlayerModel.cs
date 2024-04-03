using Assets.Scripts.ScriptableObjects;
using UnityEngine;

namespace Assets.Scripts.Player.Models
{
    public class PlayerModel : MonoBehaviour
    {
        public PlayerData playerData;

        public float RunSpeed { get; set; }

        public float JumpHeight { get; set; }

        public float GravityValue { get; set; }

        public float MaxRotationAngleY { get; set; }

        private void Awake()
        {
            RunSpeed = playerData.RunSpeed;
            JumpHeight = playerData.JumpHeight;
            GravityValue = playerData.GravityValue;
            MaxRotationAngleY = playerData.MaxRotationAngleY;
        }
    }
}
