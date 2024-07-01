using Assets._Project.Scripts.Components.Unit;
using UnityEngine;
using Voody.UniLeo;

namespace Assets._Project.Scripts.ComponentProviders.Unit
{
    public sealed class UnitAnimationComponentProvider : MonoProvider<UnitAnimationComponent>
    {
        private void Awake()
        {
            value.Animator = GetComponent<Animator>();
        }
    }
}
