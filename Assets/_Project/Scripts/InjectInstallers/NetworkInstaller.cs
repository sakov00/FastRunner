using Assets._Project.Scripts.Menu.Views;
using Assets._Project.Scripts.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;

namespace Assets._Project.Scripts.InjectInstallers
{
    public class NetworkInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<NetworkCallBacks>().FromComponentInHierarchy().AsSingle();
        }
    }
}
