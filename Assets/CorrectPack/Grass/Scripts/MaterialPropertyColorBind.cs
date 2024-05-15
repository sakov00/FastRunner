using UnityEngine;

namespace ShellTexturedGrass
{
    public class MaterialPropertyColorBind : MonoBehaviour
    {
        [SerializeField] private Material material;
        [SerializeField] private string propertyName;

        private void Awake()
        {
            Load();
        }

        public void Save(Color c)
        {
            material.SetColor(propertyName, c);
            Save();
        }

        private void Load()
        {
            var r = PlayerPrefs.GetFloat(propertyName + "r");
            var g = PlayerPrefs.GetFloat(propertyName + "g");
            var b = PlayerPrefs.GetFloat(propertyName + "b");
            Save(new Color(r, g, b));
        }

        private void Save()
        {
            PlayerPrefs.SetFloat(propertyName + "r", material.GetColor(propertyName).r);
            PlayerPrefs.SetFloat(propertyName + "g", material.GetColor(propertyName).g);
            PlayerPrefs.SetFloat(propertyName + "b", material.GetColor(propertyName).b);
        }
    }
}