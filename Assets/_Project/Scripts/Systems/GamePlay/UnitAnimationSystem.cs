using Assets._Project.Scripts.Components.GamePlay;
using Assets._Project.Scripts.Components.Physics;
using Assets._Project.Scripts.Components.Rendering;
using Leopotam.Ecs;

namespace Assets._Project.Scripts.Systems.GamePlay
{
    public class UnitAnimationSystem : IEcsRunSystem
    {
        private readonly EcsFilter<InputComponent, UnitMovementComponent, CharacterControllerComponent, UnitAnimationComponent> filter = null;

        public void Run()
        {
            foreach (var i in filter)
            {
                ref var inputComponent = ref filter.Get1(i);
                ref var unitMovementComponent = ref filter.Get2(i);
                ref var characterControllerComponent = ref filter.Get3(i);
                ref var unitAnimationComponent = ref filter.Get4(i);

                var animator = unitAnimationComponent.Animator;

                animator.SetBool(unitAnimationComponent.IsGrounded, characterControllerComponent.CharacterController.isGrounded);
                animator.SetBool(unitAnimationComponent.IsJump, unitMovementComponent.Movement.y > 0);
                animator.SetBool(unitAnimationComponent.IsFalling, unitMovementComponent.Movement.y < 0 && !characterControllerComponent.CharacterController.isGrounded);
                animator.SetFloat(unitAnimationComponent.InputZ, inputComponent.MovementInput.z);
                animator.SetFloat(unitAnimationComponent.InputX, inputComponent.MovementInput.x);
            }
        }
    }
}
