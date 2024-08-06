using Photon.Pun;
using UnityEngine;
using Voody.UniLeo;

namespace Assets._Project.Scripts.Components.Network
{
    [RequireComponent(typeof(PhotonView))]
    public class PhotonViewProvider : MonoProvider<PhotonViewComponent>
    {
        private void Awake()
        {
            value.PhotonView = GetComponent<PhotonView>();
        }
    }

    public struct PhotonViewComponent
    {
        public PhotonView PhotonView;
    }
}
