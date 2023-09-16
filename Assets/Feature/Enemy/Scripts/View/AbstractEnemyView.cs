using UnityEngine;

namespace Jam.Enemy.View.Abstaction
{
    public abstract class AbstractEnemyView : MonoBehaviour
    {
        public abstract void Walk(bool isWalk);
        public abstract void Run(bool isRun);
        public abstract void Attack();
        public abstract void Death();
        public abstract void TakeDamage();

    }
}
