using Assets._Project.Scripts.Menu.Views;
using Photon.Pun;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class SelectLevelPresenter : MonoBehaviour
{
    [Inject] private SelectLevelView view;

    void Start()
    {
        view.OnLoadFirstLvl
            .Subscribe(_ => LoadScene(1))
            .AddTo(this);

        view.OnLoadSecondLvl
            .Subscribe(_ => LoadScene(2))
            .AddTo(this);

        view.OnLoadThirdLvl
            .Subscribe(_ => LoadScene(3))
            .AddTo(this);

        view.OnLoadFourthLvl
            .Subscribe(_ => LoadScene(4))
            .AddTo(this);
    }

    private void LoadScene(int levelNumber)
    {
        PhotonNetwork.LoadLevel(levelNumber);
        PhotonNetwork.JoinRandomOrCreateRoom(null, 4);
    }
}
