using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Jam.GameInput
{
    public abstract class AbstractInput : MonoBehaviour
    {
        public abstract IReactiveProperty<bool> AttackButtonDown { get; }

        public abstract IReactiveProperty<float> X { get; }
        public abstract IReactiveProperty<float> Y { get; }

    }
}
