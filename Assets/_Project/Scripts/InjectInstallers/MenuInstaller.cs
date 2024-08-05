using Assets._Project.Scripts.Menu.Models;
using Assets._Project.Scripts.Menu.Views;
using Zenject;

namespace Assets._Project.Scripts.InjectInstallers
{
    public class MenuInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<PanelModel>().AsSingle();
            Container.Bind<PanelView>().FromComponentInHierarchy().AsSingle();
            Container.Bind<PanelPresenter>().FromComponentInHierarchy().AsSingle();

            Container.Bind<SelectLevelView>().FromComponentInHierarchy().AsSingle();
            Container.Bind<SelectLevelPresenter>().FromComponentInHierarchy().AsSingle();
        }
    }
}
