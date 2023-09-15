using Jam.Fabric.Initable.Abstraction;
using Jam.Fabric.Updator;
using Jam.Fabric.Updator.Abstraction;
using Jam.GameInput.Abstraction;
using Jam.Model;
using Jam.Model.Abstraction;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Jam.Player.Controllers
{
    using Movement;

    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private HeroView view;
        [SerializeField] private GameObject heroPrefab;
        [SerializeField] private AbstractInput input;
        [SerializeField] private PlayerModel model;

        private List<IInitializable<GameObject>> _initableObjects;
        private Updater _updator;
        private PlayerModel _playerModel;

        private void OnEnable()
        {
            _updator = new Updater();
            Initialization();
        }

        private void Update() => _updator.Update();

        private void FixedUpdate() => _updator.FixedUpdate();

        private void OnDestroy()
        {
            Destroy(_playerModel);

            for (int i = 0; i < _initableObjects.Count; i++)
                if (_initableObjects[i] is IDisposable disposble)
                    disposble.Dispose();
        }

        private void Initialization()
        {
            _initableObjects = new List<IInitializable<GameObject>>
            {
                new MovementController(),
                new RotationController()
            };

            _playerModel = Instantiate(model);

            for (int i = 0; i < _initableObjects.Count; i++)
            {
                Init<GameObject>(heroPrefab, _initableObjects[i]);
                Init<AbstractInput>(input, _initableObjects[i]);
                Init<IUpdater>(_updator, _initableObjects[i]);
                Init<IModel>(_playerModel, _initableObjects[i]);
            }
        }

        private void Init<T>(T data, object initableObject)
        {
            if (initableObject is IInitializable<T> initializable)
                initializable.Init(data);
        }
    }
}
