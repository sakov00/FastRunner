using UnityEngine;
using Voody.UniLeo;

namespace Assets._Project.Scripts.Components.Common
{
    public sealed class GameObjectProvider : MonoProvider<GameObjectComponent>
    {
        private void Awake()
        {
            value.GameObject = gameObject;
        }
    }

    public struct GameObjectComponent
    {
        public GameObject GameObject;
    }
}
