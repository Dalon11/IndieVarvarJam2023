 
using UniRx;
using UnityEngine;

namespace Jam.GameInput
{
    using Abstraction;
    public class LocalInput : AbstractInput
    {
        private readonly string _horizontal = "Horizontal";
        private readonly string _vertical = "Vertical";

        private ReactiveProperty<bool> _attackButton = new ReactiveProperty<bool>();
        private ReactiveProperty<float> _x = new ReactiveProperty<float>();
        private ReactiveProperty<float> _y = new ReactiveProperty<float>();

        public override IReadOnlyReactiveProperty<bool> AttackButton => _attackButton;

        public override IReadOnlyReactiveProperty<float> X => _x;

        public override IReadOnlyReactiveProperty<float> Y => _y;


        private void Update()
        {
            _attackButton.Value = Input.GetMouseButton(0);

            _x.Value = Input.GetAxis(_horizontal);
            _y.Value = Input.GetAxis(_vertical);
        }
    }
}
