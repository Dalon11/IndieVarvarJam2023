using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jam.Enemy.Model.Abstraction
{
    public interface IAttackEnemyModel
    {
        public float TimeAttack { get; }

        public float CoolDown { get; }

        public float Damage { get; }
    }
}
