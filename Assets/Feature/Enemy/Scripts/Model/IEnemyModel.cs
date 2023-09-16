using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Jam.Enemy.Model.Abstraction
{
    public interface IEnemyModel
    {
        public ReactiveProperty<float> Health { get; }

        public float SpeedWalk { get; }
        public float SpeedRun { get; }

        public float DistanceToAttackPlayer { get; }

        public float DistanceToChasePlayer { get; }
    }
}
