using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jam.Enemy.View
{
    using Abstaction;
    public class EnemyView : AbstractEnemyView
    {
        [SerializeField] private Animator animator;
        public override void Attack()
        {
            animator.SetTrigger("Attack");
        }

        public override void Death()
        {
            animator.SetTrigger("Death");
        }

        public override void Run(bool isRun)
        {
            animator.SetBool("Run", isRun);
        }

        public override void TakeDamage()
        {
            animator.SetTrigger("TakeDamage");
        }

        public override void Walk(bool isWalk)
        {
            animator.SetBool("WalkIdle", isWalk);
        }
    }
}