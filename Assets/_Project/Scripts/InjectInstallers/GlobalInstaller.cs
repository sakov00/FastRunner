using UnityEngine;
using Zenject;

namespace Assets._Project.Scripts.InjectInstallers
{
    public class GlobalInstaller : MonoInstaller
    {
        [SerializeField] private GameObject playerManagerSounds;

        public override void InstallBindings()
        {
        }
    }
}
