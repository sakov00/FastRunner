using Leopotam.Ecs;
using Zenject;

namespace Assets._Project.Scripts.InjectInstallers
{
    public class LevelInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindEcs();
        }

        private void BindEcs()
        {
            if(!Container.HasBinding<EcsWorld>())
                Container.Bind<EcsWorld>().AsSingle();
        }
    }
}
