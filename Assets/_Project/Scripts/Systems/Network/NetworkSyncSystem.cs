using Assets._Project.Scripts.Components.Common;
using Assets._Project.Scripts.Components.Network;
using Leopotam.Ecs;
using Photon.Pun;
using UnityEngine;

namespace Assets._Project.Scripts.Systems.Network
{
    public class NetworkSyncSystem : IEcsRunSystem
    {
        private readonly EcsFilter<TransformComponent, PhotonViewComponent> filter = null;

        public void Run()
        {
            foreach (var indexEntity in filter)
            {
                ref var transformComponent = ref filter.Get1(indexEntity);
                var photonViewComponent = filter.Get2(indexEntity);

                if (photonViewComponent.PhotonView.IsMine)
                {
                    photonViewComponent.PhotonView.RPC("SyncPosition", RpcTarget.Others, transformComponent.transform.position);
                }
            }
        }

        [PunRPC]
        public void SyncPosition(Vector3 position)
        {
            foreach (var i in filter)
            {
                var photonViewComponent = filter.Get2(i);
                if (!photonViewComponent.PhotonView.IsMine)
                {
                    ref var pos = ref filter.Get1(i);
                    pos.transform.position = position;
                }
            }
        }
    }
}
