using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jam.GameInput.Abstraction;
using Jam.Fabric.Updator.Abstraction;
using System;
using Jam.Fabric.Initable.Abstraction;
using Jam.Model.Abstraction;

namespace Jam.Player.Movement
{
    public class MovementController : IInitable<AbstractInput>, IInitable<IUpdator>, IInitable<GameObject>, IInitable<IModel>, IDisposable
    {
        private Rigidbody _rigidbody;
        private AbstractInput _input;
        private IUpdator _updator;
        private IModel _model;

        private Vector3 _newPosition;


        public void Init(AbstractInput model)
        {
            _input = model;
        }

        public void Init(IUpdator model)
        {
            _updator = model;
            _updator.OnUpdate += Move;
        }

        public void Dispose()
        {
            _updator.OnUpdate -= Move;
        }

        private void Move()
        {
            if (_model == null || _input == null)
                return;

            _newPosition = _input.X.Value * Vector3.right + _input.Y.Value * Vector3.forward;
            _newPosition = _newPosition.normalized;

            _rigidbody.AddForce(_newPosition * _model.SpeedMove, ForceMode.Force);
        }

        public void Init(GameObject model)
        {
            _rigidbody = model.GetComponent<Rigidbody>();
        }

        public void Init(IModel model)
        {
            _model = model;
        }
    }
}
