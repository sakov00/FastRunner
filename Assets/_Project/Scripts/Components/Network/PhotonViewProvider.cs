using Photon.Pun;
using Voody.UniLeo;

namespace Assets._Project.Scripts.Components.Network
{
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
