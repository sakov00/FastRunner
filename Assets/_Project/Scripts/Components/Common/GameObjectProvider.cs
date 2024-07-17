using UnityEngine;
using Voody.UniLeo;

namespace Assets._Project.Scripts.Components.Common
{
    public sealed class GameObjectProvider : MonoProvider<GameObjectComponent>
    {
        private void Awake()
        {
            value.GameObject = gameObject;
            value.IsActive = true;
        }
    }

    public struct GameObjectComponent
    {
        public GameObject GameObject;
        public bool IsActive;
    }
}
