using UnityEngine;

namespace Assets.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "DefaultData/PlayerData")]

    public class PlayerData : ScriptableObject
    {
        public float RunSpeed;

        public float JumpHeight;

        public float GravityValue;

        public float MaxRotationAngleY;
    }
}
