using System;
using UnityEngine.UI;
using Voody.UniLeo;

namespace Assets._Project.Scripts.Components.UI
{
    public sealed class CameraUIProvider : MonoProvider<CameraUIComponent> { }

    [Serializable]
    public struct CameraUIComponent
    {
        public Slider HealthSlider;
        public Slider EnergySlider;
        public Image AttentionImage;
    }
}
