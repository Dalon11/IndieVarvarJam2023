using UnityEngine;

namespace Jam.Enemy.StateMachine.State
{
    using Abstraction;
    using UnityEngine.AI;

    public class EnemyIdleState : EnemyBaseState
    {
        private NavMeshAgent _enemy;
        private Animator _animator;

        private float _timeForWalk = 7.0f;
        private float _timerForWalk;

        private float _minDistanceWalk = -2.0f;
        private float _maxDistanceWalk = 2.0f;

        private Vector3 _newPosition;

        public override void Enter()
        {
            _timerForWalk = Time.time;
        }

        public override void Update()
        {
            if(_timerForWalk + _timeForWalk < Time.time)
            {
                Walk();
            }
            else if (!_enemy.hasPath)
                _animator.SetBool("WalkIdle", false);
        }

        public override void Exit()
        {
            _animator.SetBool("WalkIdle", false);
        }

        public override void Init(NavMeshAgent enemy, GameObject player, Animator animator)
        {
            _enemy = enemy;
            _animator = animator;
        }

        private void Walk()
        {
            _animator.SetBool("WalkIdle", true);
            _timerForWalk = Time.time;

            _newPosition = _enemy.gameObject.transform.position + Random.Range(_minDistanceWalk, _maxDistanceWalk) * Vector3.right
                + Random.Range(_minDistanceWalk, _maxDistanceWalk) * Vector3.forward;

            _enemy.SetDestination(_newPosition);   
        }
    }
}
