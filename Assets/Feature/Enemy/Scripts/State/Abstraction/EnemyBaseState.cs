using UnityEngine;
using UnityEngine.AI;

namespace Jam.Enemy.StateMachine.State.Abstraction
{
    public abstract class EnemyBaseState
    {
        public abstract void Init(NavMeshAgent enemy, GameObject player, Animator animator);
        public abstract void Enter();
        public abstract void Update();
        public abstract void Exit();
    }
}

