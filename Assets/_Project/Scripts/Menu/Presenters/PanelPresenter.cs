using Assets._Project.Scripts.Menu.Views;
using UniRx;
using UnityEngine;
using Zenject;

public class PanelPresenter : MonoBehaviour
{
    [Inject] private PanelModel model;
    [Inject] private PanelView view;

    void Start()
    {
        view.OnShowPanelMainMenu
            .Subscribe(_ => model.CurrentPanel.Value = view.MainMenuPanel)
            .AddTo(this);

        view.OnShowPanelSelectLevel
            .Subscribe(_ => model.CurrentPanel.Value = view.SelectLevelPanel)
            .AddTo(this);

        view.OnShowPanelSettings
            .Subscribe(_ => model.CurrentPanel.Value = view.SettingPanel)
            .AddTo(this);
    }
}
