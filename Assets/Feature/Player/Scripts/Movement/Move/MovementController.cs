using Jam.Animation.Abstraction;
using Jam.Fabric.Initable.Abstraction;
using Jam.Fabric.Updator.Abstraction;
using Jam.GameInput.Abstraction;
using Jam.Model.Abstraction;
using System;
using UnityEngine;

namespace Jam.Player.Movement
{
    public class MovementController : IInitializable<AbstractInput>, IInitializable<IStateModel>, IInitializable<IUpdater>,
        IInitializable<GameObject>, IInitializable<IModel>, IDisposable, IInitializable<IShowMovement>
    {
        private Transform _transform;
        private Rigidbody _rigidbody;
        private AbstractInput _input;

        private IUpdater _updator;
        private IModel _model;
        private IStateModel _stateModel;
        private IShowMovement _view;

        private Vector3 _newPosition;


        public void Init(AbstractInput model) => _input = model;

        public void Init(IUpdater model)
        {
            _updator = model;
            _updator.onFixedUpdate += Move;
        }

        public void Init(GameObject model)
        {
            _transform = model.transform;
            _rigidbody = model.GetComponent<Rigidbody>();
        }

        public void Init(IShowMovement model) => _view = model;

        public void Init(IModel model) => _model = model;

        public void Init(IStateModel model) => _stateModel = model;

        private void Move()
        {
            if (_model == null || _input == null || _stateModel == null)
                return;

            if (!_stateModel.CanWalk || _stateModel.IsDeath.Value)
                return;

            _newPosition = _input.X.Value * _transform.right + _input.Y.Value * _transform.forward;
            _newPosition = _newPosition.normalized;

            _rigidbody.AddForce(_newPosition * _model.SpeedMove, ForceMode.Force);

            _view.ShowMovement(_input.X.Value, _input.Y.Value);
            _stateModel.IsWalk = Mathf.Approximately(_rigidbody.velocity.x, 0) && Mathf.Approximately(_rigidbody.velocity.z, 0);
        }

        public void Dispose() => _updator.onFixedUpdate -= Move;

    }
}
