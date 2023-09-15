using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Jam.GameInput
{
    using Abstraction;
    public class LocalInput : AbstractInput
    {
        private readonly string _horizontal = "Horizontal";
        private readonly string _vertical = "Vertical";

        private ReactiveProperty<bool> _attackButtonDown;
        private ReactiveProperty<float> _x;
        private ReactiveProperty<float> _y;

        public override IReactiveProperty<bool> AttackButtonDown => _attackButtonDown;

        public override IReactiveProperty<float> X => _x;

        public override IReactiveProperty<float> Y => _y;
        private void OnEnable()
        {
            _attackButtonDown = new ();
            _x = new();
            _y = new();
        }

        private void Update()
        {
            _attackButtonDown.Value = Input.GetMouseButtonDown(0);

            _x.Value = Input.GetAxis(_horizontal);
            _y.Value = Input.GetAxis(_vertical);
        }
    }
}
