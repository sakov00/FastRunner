using Assets._Project.Scripts.Factories;
using Zenject;

namespace Assets._Project.Scripts.InjectInstallers
{
    public class GlobalInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<PlayersFactory>().AsSingle();
            Container.Bind<SpawnObjectsFactory>().AsSingle();
            Container.Bind<EffectsFactory>().AsSingle();
        }
    }
}
