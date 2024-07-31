using Assets._Project.Scripts.Factories;
using Photon.Pun;
using UnityEngine;
using Zenject;

namespace Assets._Project.Scripts.Network
{
    public class NetworkManager : MonoBehaviourPunCallbacks, IInitializable
    {
        [Inject] PlayerFactory playerFactory;

        public void Initialize()
        {
            PhotonNetwork.ConnectUsingSettings();
        }

        public override void OnConnectedToMaster()
        {

        }

        public override void OnJoinedLobby()
        {
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
        }

        public override void OnJoinedRoom()
        {
            playerFactory.CreatePlayer(Vector3.zero);
        }
    }
}
