using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Jam.Model
{
    public interface IDecreaseHealth
    {
        public IReactiveProperty<float> Health { get; }
        public void DecreaseHealth(float value);

    }
}
