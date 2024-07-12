using Assets._Project.Scripts.Components.GamePlay;
using Assets._Project.Scripts.Components.Physics;
using Assets._Project.Scripts.Components.Rendering;
using Leopotam.Ecs;

namespace Assets._Project.Scripts.Systems.GamePlay
{
    internal class PlayerAnimationSystem : IEcsRunSystem
    {
        private readonly EcsFilter<InputComponent, UnitMovementComponent, CharacterControllerComponent, UnitAnimationComponent> filter = null;

        public void Run()
        {
            foreach (var i in filter)
            {
                ref var inputComponent = ref filter.Get1(i);
                ref var unitMovementComponent = ref filter.Get2(i);
                ref var characterControllerComponent = ref filter.Get3(i);
                ref var playerAnimationComponent = ref filter.Get4(i);

                playerAnimationComponent.Animator.SetBool(playerAnimationComponent.IsGrounded, characterControllerComponent.CharacterController.isGrounded);
                playerAnimationComponent.Animator.SetBool(playerAnimationComponent.IsJump, unitMovementComponent.Movement.y > 0);
                playerAnimationComponent.Animator.SetBool(playerAnimationComponent.IsFalling, unitMovementComponent.Movement.y < 0 && !characterControllerComponent.CharacterController.isGrounded);
                playerAnimationComponent.Animator.SetFloat(playerAnimationComponent.InputZ, inputComponent.MovementInput.z);
                playerAnimationComponent.Animator.SetFloat(playerAnimationComponent.InputX, inputComponent.MovementInput.x);
            }
        }
    }
}
