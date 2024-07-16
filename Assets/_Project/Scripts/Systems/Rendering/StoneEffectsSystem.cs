using Assets._Project.Scripts.Components.GamePlay;
using Assets._Project.Scripts.Components.Rendering;
using Leopotam.Ecs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Mathematics;
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

                foreach (var effect in effectComponent.Effects)
                {
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
                        var meshRendererGroundExplosion = groundExplosionEffect.GetComponent<MeshRenderer>();
                        var heightMaterial = meshRendererGroundExplosion.material.GetFloat("_Height");
                        heightMaterial -= 1f * Time.deltaTime;
                        meshRendererGroundExplosion.material.SetFloat("_Height", heightMaterial);

                        dustAfterFallEffect.SetActive(true);
                    }
                }

            }
        }
    }
}
