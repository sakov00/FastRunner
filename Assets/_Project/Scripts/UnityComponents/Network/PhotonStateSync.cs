using Photon.Pun;

namespace Assets._Project.Scripts.UnityComponents.Network
{
    public class PhotonStateSync : MonoBehaviourPun
    {
        [PunRPC]
        void RPC_SetActive(bool isActive)
        {
            if (photonView.IsMine)
            {
                gameObject.SetActive(isActive);
            }
        }
    }
}
