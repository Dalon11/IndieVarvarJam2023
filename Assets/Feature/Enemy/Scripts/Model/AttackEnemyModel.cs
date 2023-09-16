using UnityEngine;

namespace Jam.Enemy.Model
{
    using Abstraction;

    [CreateAssetMenu(fileName = "Jam", menuName = "AttackEnemyModel")]
    public class AttackEnemyModel : ScriptableObject, IAttackEnemyModel
    {
        [SerializeField] private float timeAttack;
        [SerializeField] private float coolDown;
        [SerializeField] private float damage;

        public float TimeAttack => timeAttack;

        public float CoolDown => coolDown;

        public float Damage => damage;
    }
}
