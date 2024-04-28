using UnityEngine;

namespace Assets._Project.Scripts.ScriptableObjects.AbilitiesData
{
    [CreateAssetMenu(fileName = "InstantAbility", menuName = "AbilityConfigs/InstantAbility")]
    public class InstantAbilityData : AbilityData
    {
        public float EnergyCost;
    }
}
