using Assets._Project.Scripts.ScriptableObjects.AbilitiesData.Abstract;
using UnityEngine;

namespace Assets._Project.Scripts.ScriptableObjects.AbilitiesData
{
    [CreateAssetMenu(fileName = "AccelerationAbilityData", menuName = "AbilityConfigs/AccelerationAbilityData")]
    public class AccelerationAbilityData : ProlongedAbilityData
    {
        public float ValueSpeedUp;
    }
}
