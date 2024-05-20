using Assets._Project.Scripts.Components.Player;
using Assets._Project.Scripts.Components.Unit;
using Leopotam.Ecs;

namespace Assets._Project.Scripts.Systems.Player
{
    internal class PlayerAnimationSystem : IEcsRunSystem
    {
        private readonly EcsFilter<InputComponent, UnitMovementComponent, UnitAnimationComponent> filter = null;

        public void Run()
        {
            foreach (var i in filter)
            {
                ref var inputComponent = ref filter.Get1(i);
                ref var unitMovementComponent = ref filter.Get2(i);
                ref var playerAnimationComponent = ref filter.Get3(i);

                playerAnimationComponent.Animator.SetBool(playerAnimationComponent.IsGrounded, unitMovementComponent.CharacterController.isGrounded);
                playerAnimationComponent.Animator.SetBool(playerAnimationComponent.IsJump, unitMovementComponent.Movement.y > 0);
                playerAnimationComponent.Animator.SetBool(playerAnimationComponent.IsFalling, unitMovementComponent.Movement.y < 0 && !unitMovementComponent.CharacterController.isGrounded);
                playerAnimationComponent.Animator.SetFloat(playerAnimationComponent.InputZ, inputComponent.MovementInput.z);
                playerAnimationComponent.Animator.SetFloat(playerAnimationComponent.InputX, inputComponent.MovementInput.x);
            }
        }
    }
}
