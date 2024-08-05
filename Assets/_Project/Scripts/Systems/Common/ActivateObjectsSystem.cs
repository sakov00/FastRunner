using Assets._Project.Scripts.Components.Common;
using Assets._Project.Scripts.Components.Network;
using Assets._Project.Scripts.Components.OneFrameComponents;
using Leopotam.Ecs;
using Photon.Pun;

namespace Assets._Project.Scripts.Systems.Common
{
    public class ActivateObjectsSystem : IEcsRunSystem
    {
        private readonly EcsFilter<GameObjectComponent, ActivateComponent, PhotonViewComponent> filter = null;

        public void Run()
        {
            foreach (var entityIndex in filter)
            {
                ref var gameObjectComponent = ref filter.Get1(entityIndex);
                ref var activateComponent = ref filter.Get2(entityIndex);
                ref var photonViewComponent = ref filter.Get3(entityIndex);

                gameObjectComponent.GameObject.SetActive(activateComponent.IsActivated);
            }
        }
    }
}
