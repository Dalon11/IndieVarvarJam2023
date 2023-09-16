using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jam.Fabric.Updator
{
    using Abstraction;
    public class Updater : IUpdater
    {
        public event Action onUpdate = () => { };
        public event Action onFixedUpdate = () => { };

        public void Update() => onUpdate.Invoke();

        public void FixedUpdate() => onFixedUpdate.Invoke();

    }
}
