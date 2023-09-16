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

    public class EnemyAttackState : EnemyBaseState, IInitializable, IInitializable<PlayerController>,
        IInitializable<AbstractEnemyView>, IInitializable<IEnemyModel>, IInitializable<IAttackEnemyModel>
    {
        private AbstractEnemyView _animator;
        private PlayerController _player;
        private IEnemyModel _model;
        private IAttackEnemyModel _attackModel;
        private ITakeDamage _takeDamage;

        private float _timeForAttack = 2.0f;
        private float _timerForAttack;

        public override void Enter()
        {
            _timerForAttack = Time.time;
        }

        public override void Update()
        {
            if (_timerForAttack + _timeForAttack < Time.time)
            {
                Attack();
            }
        }

        public override void Exit()
        {
            Debug.Log("Конец атаки");
        }
        
        private void Attack()
        {
            _animator.Attack();
            _timerForAttack = Time.time;
            _takeDamage.TakeDamage(_attackModel.Damage);
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
        #endregion
    }
}
