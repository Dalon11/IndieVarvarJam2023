using Jam.Fabric.Initable.Abstraction;
using Jam.Fabric.Updator.Abstraction;
using Jam.Model.Abstraction;
using System;
using UnityEngine;


namespace Jam.Player.Movement
{
    public class RotationController : IInitializable<IUpdater>, IInitializable<GameObject>, IInitializable<IModel>, IInitializable<IStateModel>, IDisposable
    {
        private Transform _transform;
        private IUpdater _updator;
        private IModel _model;
        private IStateModel _stateModel;
        private float _valueRotate;
        private readonly string _mouseX = "Mouse X";

        public void Init(GameObject model)
        {
            _transform = model.transform;

           // Cursor.lockState = CursorLockMode.Locked;
        }

        public void Init(IStateModel model) => _stateModel = model;

        public void Init(IUpdater model)
        {
            _updator = model;
            _updator.onUpdate += Rotating;
        }

        public void Init(IModel model) => _model = model;

        private void Rotating()
        {
            if (!_stateModel.CanWalk || _stateModel.IsDeath.Value)
                return;

            _valueRotate += Input.GetAxis(_mouseX);

            _transform.rotation = Quaternion.Euler(0f, _valueRotate * _model.ForceRotate, 0f);
        }

        public void Dispose()
        {
            _updator.onUpdate -= Rotating;

           // Cursor.lockState = CursorLockMode.Confined;
        }
    }
}