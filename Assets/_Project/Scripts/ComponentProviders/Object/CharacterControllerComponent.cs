using Assets._Project.Scripts.Components.Object;
using UnityEngine;
using Voody.UniLeo;

namespace Assets._Project.Scripts.ComponentProviders.Object
{
    public sealed class CharacterControllerComponentProvider : MonoProvider<CharacterControllerComponent>
    {
        private void Awake()
        {
            value.CharacterController = GetComponent<CharacterController>();
        }
    }
}
