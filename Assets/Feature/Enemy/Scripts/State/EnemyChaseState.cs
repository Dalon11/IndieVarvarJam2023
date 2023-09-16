using UnityEngine;
using UnityEngine.AI;

namespace Jam.Enemy.StateMachine.State
{
    using Abstraction;
    using Jam.Enemy.Model.Abstraction;
    using Jam.Enemy.View.Abstaction;
    using Jam.Fabric.Initable.Abstraction;
    using Jam.Player.Controllers;

    public class EnemyChaseState : EnemyBaseState, IInitializable, IInitializable<PlayerController>, IInitializable<NavMeshAgent>,
        IInitializable<AbstractEnemyView>, IInitializable<IEnemyModel>
    {
        private NavMeshAgent _enemy;
        private AbstractEnemyView _enemyView;
        private PlayerController _player;
        private IEnemyModel _model;

        private float _timeForCheck = 0.2f;
        private float _timerForCheck;
        private Vector3 directionRotation;

        public override void Enter()
        {
            _timerForCheck = Time.time;
            _enemy.speed = _model.SpeedRun;
            _enemyView.Run(true);
            _enemy.SetDestination(_player.transform.position);
        }

        public override void Update()
        {
            Rotation();
            if (_timerForCheck + _timeForCheck < Time.time)
            {
                _enemy.SetDestination(_player.gameObject.transform.position);
            }
        }

        public override void Exit()
        {
            _enemyView.Run(false);
        }

        private void Rotation()
        {
            directionRotation = _player.transform.position - _enemy.transform.position;
            _enemy.transform.rotation = Quaternion.Lerp(_enemy.transform.rotation, Quaternion.LookRotation(directionRotation), Time.deltaTime * 50);
        }

        #region Init
        public void Init(PlayerController model)
        {
            _player = model;
        }

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
