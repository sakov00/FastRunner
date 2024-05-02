using UnityEngine;

namespace Assets._Project.Scripts.ScriptableObjects.AbilitiesData
{
    [CreateAssetMenu(fileName = "ProlongedAbility", menuName = "AbilityConfigs/ProlongedAbility")]
    public class ProlongedAbilityData : AbilityData
    {
        public float EnergyPerSecond;

        public float EnergyTimer = 0f;
    }
}
