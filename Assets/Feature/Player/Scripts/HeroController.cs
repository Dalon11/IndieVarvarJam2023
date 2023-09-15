using System.Collections.Generic;
using UnityEngine;
using Jam.Fabric.Initable.Abstraction;
using Jam.Fabric.Updator;
using Jam.GameInput.Abstraction;

namespace Jam.Player
{
    using Fabric.Updator.Abstraction;
    using Model;
    using Movement;

    public class HeroController : MonoBehaviour
    {
        [SerializeField] private GameObject heroPrefab;
        [SerializeField] private AbstractInput input;
        [SerializeField] private PlayerModel model;

        private List<IInitable<GameObject>> _initableObjects;
        private Updator _updator;
        private PlayerModel _playerModel;

        private void OnEnable()
        {
            _updator = new Updator();
            Inization();
        }

        private void Update()
        {
            _updator.Update();
        }

        private void Inization()
        {
            _initableObjects = new List<IInitable<GameObject>>
            {
                new MovementController(),
                new RotationController()
            };
            
            _playerModel = Instantiate(model);

            for (int i = 0; i < _initableObjects.Count; i++){
                Init<GameObject>(heroPrefab, _initableObjects[i]);
                Init<AbstractInput>(input, _initableObjects[i]);
                Init<IUpdator>(_updator, _initableObjects[i]);
                Init<PlayerModel>(_playerModel, _initableObjects[i]);
            } 
        }

        private void Init<T>(T data, System.Object initializable)
        {
            if (initializable is IInitable<T> disposable)
                disposable.Init(data);
        }

        private void OnDestroy()
        {
            Destroy(_playerModel);
        }
    }
}
