using UnityEngine;

namespace Assets._Project.Scripts.ScriptableObjects.AbilitiesData.Abstract
{
    [CreateAssetMenu(fileName = "InstantAbility", menuName = "AbilityConfigs/InstantAbility")]
    public abstract class InstantAbilityData : AbilityData
    {
        public float EnergyCost;
    }
}
