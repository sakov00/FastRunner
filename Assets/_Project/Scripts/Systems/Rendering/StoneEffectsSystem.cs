using Assets._Project.Scripts.Components.GamePlay;
using Assets._Project.Scripts.Components.Rendering;
using Leopotam.Ecs;
using UnityEngine;

namespace Assets._Project.Scripts.Systems.Rendering
{
    public class StoneEffectsSystem : IEcsRunSystem
    {
        private readonly EcsFilter<EffectsComponent, GravityComponent> filter = null;

        public void Run()
        {
            foreach (var entityIndex in filter)
            {
                ref var effectComponent = ref filter.Get1(entityIndex);
                ref var gravityComponent = ref filter.Get2(entityIndex);

                var flameEffect = effectComponent.Effects[0];
                var groundExplosionEffect = effectComponent.Effects[1];
                var dustAfterFallEffect = effectComponent.Effects[2];

                if (!gravityComponent.IsGrounded)
                {
                    flameEffect.SetActive(true);
                    groundExplosionEffect.SetActive(false);
                    dustAfterFallEffect.SetActive(false);
                }
                else
                {
                    flameEffect.SetActive(false);
                    groundExplosionEffect.SetActive(true);
                    dustAfterFallEffect.SetActive(true);

                    var meshRendererGroundExplosion = groundExplosionEffect.GetComponent<MeshRenderer>();
                    var heightMaterial = meshRendererGroundExplosion.material.GetFloat("_Height");
                    heightMaterial -= Time.fixedDeltaTime;
                    meshRendererGroundExplosion.material.SetFloat("_Height", heightMaterial);                    
                }

            }
        }
    }
}
