using UnityEngine;
using UnityEngine.UI;

namespace ShellTexturedGrass
{
    public class MaterialPropertySliderBind : MonoBehaviour
    {
        [SerializeField] private Material material;
        [SerializeField] private string propertyName;
        [SerializeField] private Slider slider;
        [SerializeField] private float minValue;
        [SerializeField] private float maxValue;

        private void Awake()
        {
            slider.minValue = minValue;
            slider.maxValue = maxValue;
            Load();
        }

        private void Update()
        {
            material.SetFloat(propertyName, slider.value);
            Save();
        }

        private void OnValidate()
        {
            slider = GetComponentInChildren<Slider>();
        }

        private void Save()
        {
            if (slider.value != 0)
                PlayerPrefs.SetFloat(propertyName, slider.value);
        }

        private void Load()
        {
            slider.value = PlayerPrefs.GetFloat(propertyName);
        }
    }
}