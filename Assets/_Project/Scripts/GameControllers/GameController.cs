using Assets._Project.Scripts.Player.Models;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Assets._Project.Scripts.GameControllers
{
    public class GameController : MonoBehaviour
    {
        //private PlayerModel _playerModel;

        //[Inject]
        //private void Contract(PlayerModel playerModel)
        //{
        //    _playerModel = playerModel;
        //}

        //private void Awake()
        //{
        //    _playerModel.OnDied += PlayerDied;
        //}

        //private void PlayerDied()
        //{
        //    //ReloadCurrentScene();
        //}

        //private void ReloadCurrentScene()
        //{
        //    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        //    SceneManager.LoadSceneAsync(currentSceneIndex);
        //}
    }
}
