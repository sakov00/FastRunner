using System;
using UnityEngine;

namespace Assets._Project.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "SoundsData", menuName = "DefaultData/SoundsData")]
    public class SoundsData : ScriptableObject
    {
        [Range(0, 2)] public float OffsetPlayStepsSound;
    }
}
