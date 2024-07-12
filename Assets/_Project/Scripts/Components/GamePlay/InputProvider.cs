using UnityEngine;
using Voody.UniLeo;

namespace Assets._Project.Scripts.Components.GamePlay
{
    public sealed class InputProvider : MonoProvider<InputComponent> { }

    public struct InputComponent
    {
        public Vector3 MovementInput;
        public bool OnFirstAbility;
        public bool OnSecondAbility;
        public bool OnThirdAbility;
    }
}
