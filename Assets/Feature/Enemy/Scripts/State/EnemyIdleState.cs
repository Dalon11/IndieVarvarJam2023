using UnityEngine;
using Jam.Enemy.Model.Abstraction;
using Jam.Enemy.View.Abstaction;
using Jam.Fabric.Initable.Abstraction;
using UnityEngine.AI;

namespace Jam.Enemy.StateMachine.State
{
    using Abstraction;

    public class EnemyIdleState : EnemyBaseState, IInitializable, IInitializable<NavMeshAgent>,
        IInitializable<AbstractEnemyView>, IInitializable<IEnemyModel>
    {
        private NavMeshAgent _enemy;
        private AbstractEnemyView _enemyView;
        private IEnemyModel _model;

        private float _timeForWalk = 7.0f;
        private float _timerForWalk;

        private float _minDistanceWalk = -2.0f;
        private float _maxDistanceWalk = 2.0f;

        private Vector3 _newPosition;

        public override void Enter()
        {
            _enemy.speed = _model.SpeedWalk;
            _timerForWalk = Time.time;
        }

        public override void Update()
        {
            if(_timerForWalk + _timeForWalk < Time.time)
            {
                Walk();
            }
            else if (!_enemy.hasPath)
                _enemyView.Walk(false);
        }

        public override void Exit()
        {
            _enemyView.Walk(false);
        }

        private void Walk()
        {
            _enemyView.Walk(true);
            _timerForWalk = Time.time;

            _newPosition = _enemy.gameObject.transform.position + Random.Range(_minDistanceWalk, _maxDistanceWalk) * Vector3.right
                + Random.Range(_minDistanceWalk, _maxDistanceWalk) * Vector3.forward;

            _enemy.SetDestination(_newPosition);   
        }

        #region Init
        public void Init(NavMeshAgent model)
        {
            _enemy = model;
        }

        public void Init(AbstractEnemyView model)
        {
            _enemyView = model;
        }

        public void Init(IEnemyModel model)
        {
            _model = model;
        }
        #endregion
    }
}
