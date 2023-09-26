using UnityEngine;

namespace Jam.Model
{
    using Abstraction;
    using UniRx;

    [CreateAssetMenu(fileName = "Jam", menuName = "PlayerModel")]
    public class PlayerModel : ScriptableObject, IModel, IDecreaseHealth, IIncreaseHealth
    {
        [SerializeField] private float speedMove;
        [SerializeField] private float maxHeath;
        [SerializeField] private float forceRotate = 3f;

        private ReactiveProperty<float> _health;

        private void OnEnable()
        {
            _health = new();
            _health.Value = maxHeath;
        }

        public IReadOnlyReactiveProperty<float> Health => _health;
        public float SpeedMove => speedMove;
        public float ForceRotate => forceRotate;

        public void IncreaseHealth(float value)
        {
            if (_health.Value < maxHeath)
                _health.Value += value;
        }

        public void DecreaseHealth(float value)
        {
            if (_health.Value > 0)
                _health.Value -= value;
        }
    }
}
