using UnityEngine;
using Jam.Enemy.Model.Abstraction;
using Jam.Enemy.View.Abstaction;
using Jam.Fabric.Initable.Abstraction;
using UnityEngine.AI;
using Jam.Player.Abstraction;

namespace Jam.Enemy.StateMachine.State
{
    using Abstraction;
    using Jam.Enemy.View.Abstaction;
    using Jam.Player.Controllers;

    public class EnemyAttackState : EnemyBaseState, IInitializable, IInitializable<PlayerController>, IInitializable<NavMeshAgent>,
        IInitializable<AbstractEnemyView>, IInitializable<IEnemyModel>, IInitializable<IAttackEnemyModel>
    {
        private AbstractEnemyView _animator;
        private PlayerController _player;
        private IEnemyModel _model;
        private IAttackEnemyModel _attackModel;
        private ITakeDamage _takeDamage;
        private NavMeshAgent _enemy;

        private float _timerForAttack;
        private Vector3 StartPosition;
        private Vector3 directionRotation;

        public override void Enter()
        {
            _enemy.ResetPath();
            StartPosition = _enemy.transform.position;
        }

        public override void Update()
        {
            Rotation();
            _enemy.transform.position = StartPosition;
            if (_timerForAttack + _attackModel.CoolDown < Time.time)
            {
                Attack();
            }
        }

        public override void Exit()
        {

        }
        
        private void Attack()
        {
            _animator.Attack();
            _timerForAttack = Time.time;
            _takeDamage.TakeDamage(_attackModel.Damage);
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
            
            _takeDamage = _player.GetComponent<PlayerController>().GetController<ITakeDamage>();
        }

        public void Init(IAttackEnemyModel model)
        {
            _attackModel = model;
        }

        public void Init(IEnemyModel model)
        {
            _model = model;
        }

        public void Init(AbstractEnemyView model)
        {
            _animator = model;
        }

        public void Init(NavMeshAgent model)
        {
            _enemy = model;
        }
        #endregion
    }
}
