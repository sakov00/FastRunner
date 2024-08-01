using Assets._Project.Scripts.Components.Common;
using Assets._Project.Scripts.Components.Network;
using Leopotam.Ecs;
using Photon.Pun;

namespace Assets._Project.Scripts.Systems.Common
{
    public class ActivateObjectsSystem : IEcsRunSystem
    {
        private readonly EcsFilter<GameObjectComponent, PhotonViewComponent> filter = null;

        public void Run()
        {
            foreach (var entityIndex in filter)
            {
                ref var gameObjectComponent = ref filter.Get1(entityIndex);
                ref var photonViewComponent = ref filter.Get2(entityIndex);

                gameObjectComponent.GameObject.SetActive(gameObjectComponent.IsActive);
                photonViewComponent.PhotonView.RPC("RPC_SetActive", RpcTarget.AllBuffered, gameObjectComponent.IsActive);
            }
        }
    }
}
