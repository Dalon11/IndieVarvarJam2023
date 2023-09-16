using Jam.Player.Abstraction;
using System;
using UnityEngine;

namespace Jam.Attack
{
    public class Weapon : MonoBehaviour
    {
        public event Action<ITakeDamage> onTriggerEnter = delegate { };

        private void OnTriggerEnter(Collider other)
        {
            if (other is ITakeDamage controller)
            {
                onTriggerEnter.Invoke(controller);
            }
        }
    }
}
