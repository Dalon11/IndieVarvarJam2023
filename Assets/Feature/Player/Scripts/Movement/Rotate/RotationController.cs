using Jam.Fabric.Initable.Abstraction;
using Jam.Fabric.Updator.Abstraction;
using Jam.Model.Abstraction;
using System;
using UnityEngine;


namespace Jam.Player.Movement
{
    public class RotationController : IInitializable<IUpdater>, IInitializable<GameObject>, IInitializable<IModel>, IDisposable
    {
        private Transform _transform;
        private IUpdater _updator;
        private IModel _model;

        private float _valueRotate;

        public void Init(GameObject model)
        {
            _transform = model.transform;
        }

        public void Init(IUpdater model)
        {
            _updator = model;
            _updator.onUpdate += Rotating;
        }

        public void Init(IModel model)
        {
            _model = model;
        }
        private void Rotating()
        {
            _valueRotate += Input.GetAxis("Mouse X");

            _transform.rotation = Quaternion.Euler(0f, _valueRotate * _model.ForceRotate, 0f);
        }

        public void Dispose()
        {
            _updator.onUpdate -= Rotating;
        }
    }
}