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
        [SerializeField] private float speedMove;
        [SerializeField] private float cooldown;
        [SerializeField] private float speedAttack;
        [SerializeField] private float damage;
        [SerializeField] private float maxHeath;
        [SerializeField] private float forceRotate = 3f;

        private ReactiveProperty<float> _health;

        public PlayerModel()
        {
            _health = new();
            _health.Value = maxHeath;
        }

        public IReactiveProperty<float> Health => _health;

        public float SpeedMove => speedMove;

        public float Cooldown => cooldown;

        public float SpeedAttack => speedAttack;

        public float Damage => damage;

        public float ForceRotate => forceRotate;
    }
}
