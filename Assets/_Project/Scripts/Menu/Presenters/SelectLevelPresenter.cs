using Assets._Project.Scripts.Menu.Views;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class SelectLevelPresenter : MonoBehaviour
{
    [Inject] private SelectLevelView view;

    private List<string> ListLevels = new List<string>();

    void Start()
    {
        GetListLevels();

        view.OnLoadFirstLvl
            .Subscribe(_ => LoadScene(ListLevels[1]))
            .AddTo(this);

        view.OnLoadSecondLvl
            .Subscribe(_ => LoadScene(ListLevels[2]))
            .AddTo(this);

        view.OnLoadThirdLvl
            .Subscribe(_ => LoadScene(ListLevels[3]))
            .AddTo(this);

        view.OnLoadFourthLvl
            .Subscribe(_ => LoadScene(ListLevels[4]))
            .AddTo(this);
    }

    private void GetListLevels()
    {
        int sceneCount = SceneManager.sceneCountInBuildSettings;

        for (int i = 0; i < sceneCount; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
            ListLevels.Add(sceneName);
        }
    }

    private void LoadScene(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
}
