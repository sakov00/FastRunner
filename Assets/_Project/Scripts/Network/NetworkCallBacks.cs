using Assets._Project.Scripts.Factories;
using Assets._Project.Scripts.Interfaces;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets._Project.Scripts.Network
{
    public class NetworkCallBacks : MonoBehaviourPunCallbacks
    {
        [Inject] List<ICustomInitializable> _initializableList;
        [Inject] PlayerFactory playerFactory;

        public void Awake()
        {
            PhotonNetwork.ConnectUsingSettings();
        }

        public override void OnConnectedToMaster()
        {
            PhotonNetwork.JoinLobby();
            Debug.Log("OnConnectedToMaster");
        }

        public override void OnJoinedLobby()
        {
            PhotonNetwork.JoinRandomRoom();
            Debug.Log("OnJoinedLobby");
        }

        public override void OnCreatedRoom()
        {
            Debug.Log("OnCreatedRoom");
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 4 });
        }

        public override void OnJoinedRoom()
        {
            _initializableList.ForEach(x => x.Initialize());
            playerFactory.CreatePlayer(Vector3.zero);
        }
    }
}
