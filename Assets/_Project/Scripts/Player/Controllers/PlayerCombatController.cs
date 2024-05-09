using Assets._Project.Scripts.Enums;
using Assets._Project.Scripts.Player.Models;
using UnityEngine;
using Zenject;

namespace Assets._Project.Scripts.Player.Controllers
{
    public class PlayerCombatController : MonoBehaviour
    {
        private PlayerModel _playerModel;

        private void Awake()
        {
            _playerModel = GetComponent<PlayerModel>();
        }

        //need optimization
        void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if (hit.gameObject.GetComponent<DamageComponent>() is DamageComponent damageComponent)
            {
                switch (damageComponent.DamageType)
                {
                    case DamageType.Energy:
                        TakeEnergyDamage(damageComponent.EnergyDamage);
                        break;
                    case DamageType.Fatal:
                        TakeFatalDamage();
                        break;
                    default:
                        break;
                }
            }
        }

        void TakeEnergyDamage(float damage)
        {
            _playerModel.EnergyValue -= damage;
        }

        void TakeFatalDamage()
        {
            _playerModel.Die();
        }
    }
}
