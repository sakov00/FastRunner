using Assets._Project.Scripts.Components.Object;
using Voody.UniLeo;

namespace Assets._Project.Scripts.ComponentProviders.Object
{
    public sealed class GameObjectProvider : MonoProvider<GameObjectComponent>
    {
        private void Awake()
        {
            value.GameObject = gameObject;
        }
    }
}
