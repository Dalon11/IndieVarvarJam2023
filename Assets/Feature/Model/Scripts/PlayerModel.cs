using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Jam.Model
{
    using Abstraction;
    using UniRx;
    
    [CreateAssetMenu(fileName = "Jam", menuName = "PlayerModel")]
    public class PlayerModel : ScriptableObject, IModel
    {
        public IReactiveProperty<float> Health => _health;

        public float SpeedMove => speedMove;

        public float Cooldown => cooldown;

        public float SpeedAttack => speedAttack;

        public float Damage => damage;

        public PlayerModel()
        {
            _health = new();
            _health.Value = maxHeath;
        }

        [SerializeField] private float speedMove;
        [SerializeField] private float cooldown;
        [SerializeField] private float speedAttack;
        [SerializeField] private float damage;
        [SerializeField] private float maxHeath;

        private ReactiveProperty<float> _health;

    }
}
