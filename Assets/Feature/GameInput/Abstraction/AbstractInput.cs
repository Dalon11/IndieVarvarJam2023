using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Jam.GameInput.Abstraction
{
    public abstract class AbstractInput : MonoBehaviour
    {
        public abstract IReadOnlyReactiveProperty<bool> AttackButton { get; }

        public abstract IReadOnlyReactiveProperty<float> X { get; }
        public abstract IReadOnlyReactiveProperty<float> Y { get; }

    }
}
