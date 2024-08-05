using UniRx;
using UnityEngine;

namespace Assets._Project.Scripts.Menu.Models
{
    public class PanelModel
    {
        public ReactiveProperty<GameObject> CurrentPanel { get; set; }

        public PanelModel()
        {
            CurrentPanel = new ReactiveProperty<GameObject>();
        }
    }
}
