using Assets._Project.Scripts.Components.Player;
using Leopotam.Ecs;
using UnityEngine;

namespace Assets._Project.Scripts.Systems.Init
{
    public class InputMobileSystem : IEcsInitSystem, IEcsDestroySystem
    {
        private PlayerInput _inputActions;
        private EcsFilter<InputComponent> filter = null;

        public void Init()
        {
            _inputActions = new PlayerInput();
            _inputActions.Enable();

            _inputActions.Mobile.Movement.performed += Movement;
            _inputActions.Mobile.Movement.canceled += Movement;
            _inputActions.Mobile.Jump.performed += Jump;
            _inputActions.Mobile.Jump.canceled += Jump;
            _inputActions.Mobile.FirstAbility.performed += FirstAbility;
            _inputActions.Mobile.SecondAbility.performed += SecondAbility;
            _inputActions.Mobile.ThirdAbility.performed += ThirdAbility;
        }

        public void Destroy()
        {
            _inputActions.Mobile.Movement.performed -= Movement;
            _inputActions.Mobile.Movement.canceled -= Movement;
            _inputActions.Mobile.Jump.performed -= Jump;
            _inputActions.Mobile.Jump.canceled -= Jump;
            _inputActions.Mobile.FirstAbility.performed -= FirstAbility;
            _inputActions.Mobile.SecondAbility.performed -= SecondAbility;
            _inputActions.Mobile.ThirdAbility.performed -= ThirdAbility;

            _inputActions.Disable();
        }

        private void Movement(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            foreach (var i in filter)
            {
                ref var inputComponent = ref filter.Get1(i);
                var movement = obj.ReadValue<Vector2>();
                inputComponent.MovementInput = new Vector3(movement.x, 0, movement.y);
            }
        }

        private void Jump(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            foreach (var i in filter)
            {
                ref var inputComponent = ref filter.Get1(i);
                var movementY = obj.ReadValue<float>();
                inputComponent.MovementInput = new Vector3(0, movementY, 0);
            }
        }

        private void FirstAbility(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            UpdateAbilityInput(1, false);
        }

        private void SecondAbility(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            UpdateAbilityInput(2, false);
        }

        private void ThirdAbility(UnityEngine.InputSystem.InputAction.CallbackContext obj)
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
