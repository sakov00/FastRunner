using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Project.Scripts.Menu.Views
{
    public class SelectLevelView : MonoBehaviour
    {
        [SerializeField] private Button buttonFirstLvl;
        [SerializeField] private Button buttonSecondLvl;
        [SerializeField] private Button buttonThirdLvl;
        [SerializeField] private Button buttonFourthLvl;

        public IObservable<Unit> OnLoadFirstLvl => buttonFirstLvl.OnClickAsObservable();
        public IObservable<Unit> OnLoadSecondLvl => buttonSecondLvl.OnClickAsObservable();
        public IObservable<Unit> OnLoadThirdLvl => buttonThirdLvl.OnClickAsObservable();
        public IObservable<Unit> OnLoadFourthLvl => buttonFourthLvl.OnClickAsObservable();
    }
}
