using Assets._Project.Scripts.Components.GamePlay;
using Leopotam.Ecs;
using UnityEngine;

namespace Assets._Project.Scripts.Systems.GamePlay.InputDevice
{
    public class InputPCSystem : IEcsInitSystem, IEcsDestroySystem
    {
        private PlayerInput _inputActions;
        private EcsFilter<InputComponent> filter = null;

        public void Init()
        {
            _inputActions = new PlayerInput();
            _inputActions.Enable();

            _inputActions.PC.Movement.performed += Movement;
            _inputActions.PC.Movement.canceled += Movement;
            _inputActions.PC.FirstAbility.performed += FirstAbilityAcivated;
            _inputActions.PC.SecondAbility.performed += SecondAbilityAcivated;
            _inputActions.PC.ThirdAbility.performed += ThirdAbilityAcivated;
            _inputActions.PC.FirstAbility.canceled += FirstAbilityDeactivated;
            _inputActions.PC.SecondAbility.canceled += SecondAbilityDeactivated;
            _inputActions.PC.ThirdAbility.canceled += ThirdAbilityDeactivated;
        }

        public void Destroy()
        {
            _inputActions.PC.Movement.performed -= Movement;
            _inputActions.PC.Movement.canceled -= Movement;
            _inputActions.PC.FirstAbility.performed -= FirstAbilityAcivated;
            _inputActions.PC.SecondAbility.performed -= SecondAbilityAcivated;
            _inputActions.PC.ThirdAbility.performed -= ThirdAbilityAcivated;
            _inputActions.PC.FirstAbility.canceled -= FirstAbilityDeactivated;
            _inputActions.PC.SecondAbility.canceled -= SecondAbilityDeactivated;
            _inputActions.PC.ThirdAbility.canceled -= ThirdAbilityDeactivated;

            _inputActions.Disable();
        }

        private void Movement(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            foreach (var i in filter)
            {
                ref var inputComponent = ref filter.Get1(i);
                inputComponent.MovementInput = obj.ReadValue<Vector3>();
            }
        }

        private void FirstAbilityAcivated(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            UpdateAbilityInput(1, true);
        }

        private void SecondAbilityAcivated(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            UpdateAbilityInput(2, true);
        }

        private void ThirdAbilityAcivated(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            UpdateAbilityInput(3, true);
        }

        private void FirstAbilityDeactivated(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            UpdateAbilityInput(1, false);
        }

        private void SecondAbilityDeactivated(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            UpdateAbilityInput(2, false);
        }

        private void ThirdAbilityDeactivated(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            UpdateAbilityInput(3, false);
        }

        private void UpdateAbilityInput(int abilityIndex, bool state)
        {
            foreach (var i in filter)
            {
                ref var inputComponent = ref filter.Get1(i);
                switch (abilityIndex)
                {
                    case 1:
                        inputComponent.OnFirstAbility = state;
                        break;
                    case 2:
                        inputComponent.OnSecondAbility = state;
                        break;
                    case 3:
                        inputComponent.OnThirdAbility = state;
                        break;
                }
            }
        }
    }
}
