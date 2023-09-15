using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jam.Fabric.Updator
{
    using Abstraction;
    public class Updator : IUpdator
    {
        public event Action OnUpdate;

        public void Update()
        {
            OnUpdate.Invoke();
        }
    }
}
