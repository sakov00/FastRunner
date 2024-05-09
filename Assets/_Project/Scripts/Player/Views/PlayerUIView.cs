using Assets._Project.Scripts.Player.Models;
using UnityEngine;
using Zenject;
using UnityEngine.UI;

namespace Assets._Project.Scripts.Player.Views
{
    public class PlayerUIView : MonoBehaviour
    {
        private PlayerModel _playerModel;
        [SerializeField] private Slider _sliderEnergy;

        private void Awake()
        {
            _playerModel = GetComponent<PlayerModel>();
        }

        private void Start()
        {
            _playerModel.PropertyChanged += playerModel_PropertyChanged;
        }

        private void playerModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(PlayerModel.EnergyValue))
                _sliderEnergy.value = _playerModel.EnergyValue;
        }
    }
}
