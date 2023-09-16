using UnityEngine;
using UnityEngine.AI;
using Jam.Enemy.View.Abstaction;
using Jam.Enemy.Model.Abstraction;

namespace Jam.Enemy.StateMachine.State.Abstraction
{
    public abstract class EnemyBaseState
    {
        public abstract void Enter();
        public abstract void Update();
        public abstract void Exit();
    }
}

