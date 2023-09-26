using Jam.GameInput.Abstraction;
using Jam.Player;
using Jam.Player.Abstraction;
using Jam.Player.Controllers;
using UniRx;
using UnityEngine;

namespace Jam.Attack
{
    public class CharacterAttackController : MonoBehaviour
    {
        [SerializeField] Weapon weapon;
        [SerializeField] PlayerController playerController;
        [SerializeField] AbstractInput input;

        private IMakeDamage damageController;

        private void Start()
        {
            input.AttackButton.Where(x => x).Subscribe(_ => Attack()).AddTo(this);

            damageController = playerController.GetController<IMakeDamage>();
            weapon.onTriggerEnter += damageController.MakeDamage;
        }

        private void OnDestroy()
        {
            weapon.onTriggerEnter -= damageController.MakeDamage;
        }

        public void Attack()
        {
            //weapon.SetActiveCollider(true);
           // view.ShowAttack();
        }

    }
}
