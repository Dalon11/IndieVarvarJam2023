using UnityEngine;
using UnityEngine.AI;

namespace Jam.Enemy.StateMachine
{
    using State;
    using State.Abstraction;
    public class EnemyStateMachine : MonoBehaviour
    {
        [SerializeField] private float distanceToAttackPlayer;
        [SerializeField] private float distanceToChasePlayer;
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private Animator animator;

        private EnemyAttackState _attackState;
        private EnemyChaseState _chaseState;
        private EnemyIdleState _idleState;

        private EnemyBaseState _currentState;
        private GameObject _player;
        private Vector3 directionToPlayer;

        private void OnEnable()
        {
            _player = GameObject.Find("Player");/// Поиск надо поменять.
            Init();
        }

        private void Start()
        {
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
            _idleState = new EnemyIdleState();
            _idleState.Init(agent, null, animator);

            _chaseState = new EnemyChaseState();
            _chaseState.Init(agent, _player, animator);

            _attackState = new EnemyAttackState();
            _attackState.Init(agent, _player, animator);
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
            if (directionToPlayer.sqrMagnitude < distanceToChasePlayer * distanceToChasePlayer)
            {
                if (directionToPlayer.sqrMagnitude < distanceToAttackPlayer * distanceToAttackPlayer
                    && !(_currentState is EnemyAttackState))
                    ChangeState(_attackState);
                else if (directionToPlayer.sqrMagnitude > distanceToAttackPlayer * distanceToAttackPlayer
                    && !(_currentState is EnemyChaseState))
                    ChangeState(_chaseState);
            }
            else if (!(_currentState is EnemyIdleState))
                ChangeState(_idleState);
        }
    }
}
