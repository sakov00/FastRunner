using UnityEngine;
using Voody.UniLeo;

namespace Assets._Project.Scripts.Components.Physics
{
    public sealed class CharacterControllerProvider : MonoProvider<CharacterControllerComponent>
    {
        private void Awake()
        {
            value.CharacterController = GetComponent<CharacterController>();
        }
    }

    public struct CharacterControllerComponent
    {
        public CharacterController CharacterController;
    }
}
