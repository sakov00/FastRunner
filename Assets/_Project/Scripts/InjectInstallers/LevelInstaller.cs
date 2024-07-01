using Assets._Project.Scripts.Factories;
using Zenject;

namespace Assets._Project.Scripts.InjectInstallers
{
    public class LevelInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindFactories();
            //BindSpawners();
        }

        private void BindFactories()
        {
            Container.Bind<PlayerFactory>().AsSingle();
        }

        private void BindSpawners()
        {
            Container.Bind<StoneSpawner>().AsSingle();
        }
    }
}
