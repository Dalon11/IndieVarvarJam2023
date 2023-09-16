using UnityEngine;
using UnityEngine.AI;

namespace Jam.Enemy.StateMachine.State
{
    using Abstraction;

    public class EnemyChaseState : EnemyBaseState
    {
        private NavMeshAgent _enemy;
        private Animator _animator;
        private GameObject _player;

        private float _timeForCheck = 0.2f;
        private float _timerForCheck;

        public override void Enter()
        {
            _timerForCheck = Time.time;
            _animator.SetBool("Run", true);
            _enemy.SetDestination(_player.transform.position);
        }

        public override void Update()
        {
            if (_timerForCheck + _timeForCheck < Time.time)
            {
                _enemy.SetDestination(_player.transform.position);
            }
        }

        public override void Exit()
        {
            _animator.SetBool("Run", false);
        }

        public override void Init(NavMeshAgent enemy, GameObject player, Animator animator)
        {
            _enemy = enemy;
            _player = player;
            _animator = animator;
        }
    }
}
