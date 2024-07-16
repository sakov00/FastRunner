using Assets._Project.Scripts.Factories;
using Zenject;

namespace Assets._Project.Scripts.InjectInstallers
{
    public class LevelInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            //BindObjectPools();
            //BindFactories();
        }

        private void BindObjectPools()
        {
            Container.Bind<ObjectPool>().AsSingle();
        }

        private void BindFactories()
        {
            Container.Bind<PlayerFactory>().AsSingle();
            Container.Bind<StonesFactory>().AsSingle();
        }
    }
}
