using Jam.Model;
using UniRx;
using UnityEngine;

namespace Jam.Enemy.Model
{
    using Abstraction;

    [CreateAssetMenu(fileName = "Jam", menuName = "EnemyModel")]
    public class EnemyModel : ScriptableObject, IEnemyModel, IDecreaseHealth
    {

        [SerializeField] private float maxHealth;
        [SerializeField] private float speedWalk;
        [SerializeField] private float speedRun;
        [SerializeField] private float distanceToAttackPlayer;
        [SerializeField] private float distanceToChasePlayer;

        private ReactiveProperty<float> _health;

        public IReactiveProperty<float> Health => _health;

        public float DistanceToAttackPlayer => distanceToAttackPlayer;

        public float DistanceToChasePlayer => distanceToChasePlayer;

        public float SpeedWalk => speedWalk;

        public float SpeedRun => speedRun;

        public EnemyModel()
        {
            _health = new();
            _health.Value = maxHealth;
        }

        public void DecreaseHealth(float value) => _health.Value -= value;
    }
}
