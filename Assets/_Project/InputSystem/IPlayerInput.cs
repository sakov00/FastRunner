using System;
using UnityEngine;

namespace Assets._Project.InputSystem
{
    public interface IPlayerInput
    {
        public Vector3 MovementInput { get; set; }
        public event Action OnFirstAbility;
        public event Action OnSecondAbility;
        public event Action OnThirdAbility;

        public void OnEnable();
        public void OnDisable();
    }
}
