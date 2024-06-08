using Assets._Project.Scripts.Player.Models;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets._Project.Scripts.GameControllers
{
    public class GameController : MonoBehaviour
    {
        private PlayerModel _playerModel;

        private void Start()
        {
            _playerModel = FindFirstObjectByType<PlayerModel>();

            _playerModel.OnDied += PlayerDied;
        }

        private void OnDisable()
        {
            _playerModel.OnDied -= PlayerDied;
        }

        private void PlayerDied()
        {
            ReloadCurrentScene();
        }

        private void ReloadCurrentScene()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadSceneAsync(currentSceneIndex);
        }
    }
}
