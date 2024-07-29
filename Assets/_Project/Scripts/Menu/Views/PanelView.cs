using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;
using Zenject;

namespace Assets._Project.Scripts.Menu.Views
{
    public class PanelView : MonoBehaviour
    {
        [Inject] private PanelModel model;

        [field: SerializeField] public GameObject MainMenuPanel { get; private set; }
        [field: SerializeField] public GameObject SelectLevelPanel { get; private set; }
        [field: SerializeField] public GameObject SettingPanel { get; private set; }
        [field: SerializeField] public GameObject ConstantPanel { get; private set; }
        

        [SerializeField] private Button buttonMainMenu;
        [SerializeField] private Button buttonSelectLevel;
        [SerializeField] private Button buttonSettings;

        public IObservable<Unit> OnShowPanelMainMenu => buttonMainMenu.OnClickAsObservable();
        public IObservable<Unit> OnShowPanelSelectLevel => buttonSelectLevel.OnClickAsObservable();
        public IObservable<Unit> OnShowPanelSettings => buttonSettings.OnClickAsObservable();

        private void Start()
        {
            model.CurrentPanel.Value = MainMenuPanel;
            model.CurrentPanel.Subscribe(_ => UpdatePanels()).AddTo(this);
        }

        private void UpdatePanels()
        {
            HideAllPanels();

            if (model.CurrentPanel.HasValue)
            {
                model.CurrentPanel.Value.SetActive(true);
                ConstantPanel.SetActive(!MainMenuPanel.activeInHierarchy);
            }
        }

        private void HideAllPanels()
        {
            MainMenuPanel.SetActive(false);
            SelectLevelPanel.SetActive(false);
            SettingPanel.SetActive(false);
        }
    }
}
