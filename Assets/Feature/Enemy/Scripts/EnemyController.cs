using System;
using UnityEngine;
using UnityEngine.AI;
using Jam.Enemy.Model.Abstraction;
using Jam.Enemy.Model;
using System.Collections.Generic;
using Jam.Enemy.View;
using Jam.Enemy.View.Abstaction;
using Jam.Fabric.Initable.Abstraction;
using Jam.Player.Abstraction;
using Jam.Player.Controllers;

namespace Jam.Enemy.StateMachine
{
    using State;
    using State.Abstraction;
    public class EnemyController : MonoBehaviour, ITakeDamage
    {
        [SerializeField] private PlayerController _player; /// Поиск надо поменять.
        [SerializeField] private EnemyModel enemyModel;
        [SerializeField] private AttackEnemyModel attackModel;
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private EnemyView animator;

        private EnemyModel _enemyModel;
        private AttackEnemyModel _attackModel;

        private List<IInitializable> _enemyStates;
        private EnemyAttackState _attackState;
        private EnemyChaseState _chaseState;
        private EnemyIdleState _idleState;

        private EnemyBaseState _currentState;
        private Vector3 directionToPlayer;

        private void Start()
        {
            Init();
            _currentState = _idleState;
        }

        private void Update()
        {
            _currentState.Update();
            if (_player != null)
                DistanceCalculation();
        }

        private void Init()
        {
            _enemyModel = Instantiate(enemyModel);
            _attackModel = Instantiate(attackModel);

            _idleState = new EnemyIdleState();
            _chaseState = new EnemyChaseState();
            _attackState = new EnemyAttackState();
            _enemyStates = new();

            _enemyStates.Add(_idleState);
            _enemyStates.Add(_chaseState);
            _enemyStates.Add(_attackState);
            for(int i =0; i < _enemyStates.Count; i++)
            {
                Init<PlayerController>(_player, _enemyStates[i]);
                Init<AbstractEnemyView>(animator, _enemyStates[i]);
                Init<NavMeshAgent>(agent, _enemyStates[i]);
                Init<IEnemyModel>(_enemyModel, _enemyStates[i]);
                Init<IAttackEnemyModel>(_attackModel, _enemyStates[i]);
            }
        }

        private void ChangeState(EnemyBaseState newState)
        {
            _currentState.Exit();
            _currentState = newState;
            _currentState.Enter();
        }

        private void DistanceCalculation()
        {
            directionToPlayer = _player.transform.position - transform.position;
            if (directionToPlayer.sqrMagnitude < enemyModel.DistanceToChasePlayer * enemyModel.DistanceToChasePlayer)
            {
                if (directionToPlayer.sqrMagnitude < enemyModel.DistanceToAttackPlayer * enemyModel.DistanceToAttackPlayer
                    && !(_currentState is EnemyAttackState))
                    ChangeState(_attackState);
                else if (directionToPlayer.sqrMagnitude > enemyModel.DistanceToAttackPlayer * enemyModel.DistanceToAttackPlayer
                    && !(_currentState is EnemyChaseState))
                    ChangeState(_chaseState);
            }
            else if (!(_currentState is EnemyIdleState))
                ChangeState(_idleState);
        }

        private void Init<T>(T data, IInitializable initializableObject)
        {
            if (initializableObject is IInitializable<T> initializable)
                initializable.Init(data);
        }
        private void OnDestroy()
        {
            Destroy(_attackModel);
            Destroy(_enemyModel);
        }

        public void TakeDamage(float damage)
        {
            _enemyModel.Health.Value -= damage;
        }
    }
}
