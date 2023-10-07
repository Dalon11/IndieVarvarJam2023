using Jam.Player.Abstraction;
using System;
using UnityEngine;

namespace Jam.Player
{
    public class Weapon : MonoBehaviour
    {        
        public event Action<ITakeDamage> onTriggerEnter = delegate { };
        private ITakeDamage _takeDamageController;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<ITakeDamage>(out _takeDamageController))
            {
                onTriggerEnter.Invoke(_takeDamageController);
            }
        }

    }
}
