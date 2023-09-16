using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jam.Model.Abstraction
{
    public interface IAttackModel
    {

        public float Cooldown { get; }

        public float SpeedAttack { get; }

        public float Damage { get; }
    }
}
