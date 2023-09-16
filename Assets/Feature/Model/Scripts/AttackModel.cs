using Jam.Model.Abstraction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jam.Model
{
    [CreateAssetMenu(fileName = nameof(AttackModel), menuName = "Data/" + nameof(AttackModel))]
    public class AttackModel : ScriptableObject, IAttackModel
    {
        [SerializeField] private float cooldown;
        [SerializeField] private float speedAttack;
        [SerializeField] private float damage;

        public float Cooldown => cooldown;

        public float SpeedAttack => speedAttack;

        public float Damage => damage;
    }
}
