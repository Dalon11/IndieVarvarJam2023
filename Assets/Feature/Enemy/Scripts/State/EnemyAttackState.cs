using UnityEngine.AI;
using UnityEngine;

namespace Jam.Enemy.StateMachine.State
{
    using Abstraction;
    

    public class EnemyAttackState : EnemyBaseState
    {
        private NavMeshAgent _enemy;
        private Animator _animator;
        private GameObject _player;

        private float _timeForAttack = 7.0f;
        private float _timerForAttack;

        public override void Enter()
        {
            Debug.Log("Начало атаки");
        }

        public override void Update()
        {
            Debug.Log("атака");
        }

        public override void Exit()
        {
            Debug.Log("Конец атаки");
        }

        public override void Init(NavMeshAgent enemy, GameObject player, Animator animator)
        {
            _enemy = enemy;
            _player = player;
            _animator = animator;
        }
    }
}
