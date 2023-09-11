using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Jam.GameInput
{
    public class LocalInput : AbstractInput
    {
        private readonly string _horizontal = "Horizontal";
        private readonly string _vertical = "Vertical";
        public override IReactiveProperty<bool> AttackButtonDown => throw new System.NotImplementedException();


        public override IReactiveProperty<float> X => throw new System.NotImplementedException();

        public override IReactiveProperty<float> Y => throw new System.NotImplementedException();

        private void Update()
        {
            AttackButtonDown.Value = Input.GetMouseButtonDown(0);

            X.Value = Input.GetAxis(_horizontal);
            Y.Value = Input.GetAxis(_vertical);

        }
    }
}
