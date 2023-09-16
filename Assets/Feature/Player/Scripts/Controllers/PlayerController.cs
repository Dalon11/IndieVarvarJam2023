using Jam.Fabric.Initable.Abstraction;
using Jam.Fabric.Updator;
using Jam.Fabric.Updator.Abstraction;
using Jam.GameInput.Abstraction;
using Jam.Model;
using Jam.Model.Abstraction;
using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Jam.Player.Controllers
{
    using Movement;
    using Jam.Animation;
    using Jam.Animation.Abstraction;

    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private GameObject heroPrefab;
        [SerializeField] private AbstractInput input;
        [Space]
        [SerializeField] private PlayerModel model;
        [SerializeField] private StateModel stateModel;
        [SerializeField] private AttackModel attackModel;
        [Space]
        [SerializeField] private Weapon weapon;
        [SerializeField] private AnimatorProvider animatorProvider;

        private List<IInitializable> _initializableObjects;
        private Updater _updator;
        private PlayerModel _playerModel;

        private void Awake()
        {
            Initialization();
            stateModel.IsDeath.Value = false;
            stateModel.IsDeath.Subscribe(x =>
            {
                Debug.LogError($"isdeat + {x}");
            }).AddTo(this);
        }

        private void Update() => _updator.Update();

        private void FixedUpdate() => _updator.FixedUpdate();

        private void OnDestroy()
        {
            Destroy(_playerModel);

            for (int i = 0; i < _initializableObjects.Count; i++)
                if (_initializableObjects[i] is IDisposable disposble)
                    disposble.Dispose();
        }

        private void Initialization()
        {
            _updator = new Updater();

            _initializableObjects = new List<IInitializable>
            {
                new MovementController(),
                new RotationController(),
                new CharacterDamageController(),
                new AttackController(weapon)
            };

            _playerModel = Instantiate(model);

            for (int i = 0; i < _initializableObjects.Count; i++)
            {
                Init<GameObject>(heroPrefab, _initializableObjects[i]);
                Init<AbstractInput>(input, _initializableObjects[i]);
                Init<IUpdater>(_updator, _initializableObjects[i]);


                Init<IShowAttack>(animatorProvider, _initializableObjects[i]);
                Init<IShowMovement>(animatorProvider, _initializableObjects[i]);

                Init<IModel>(_playerModel, _initializableObjects[i]);
                Init<IDecreaseHealth>(_playerModel, _initializableObjects[i]);

                Init<IStateModel>(stateModel, _initializableObjects[i]);

                Init<IAttackModel>(attackModel, _initializableObjects[i]);
            }
        }

        private void Init<T>(T data, IInitializable initializableObject)
        {
            if (initializableObject is IInitializable<T> initializable)
                initializable.Init(data);
        }

        public T GetController<T>()
        {
            for (int i = 0; i < _initializableObjects.Count; i++)
                if (_initializableObjects[i] is T controller)
                    return controller;

            return default;
        }
    }
}
