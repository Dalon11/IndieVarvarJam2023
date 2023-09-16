using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jam.Model.Abstraction
{
    public interface IStateModel
    {
        public bool IsWalk { get; set; }
        public bool IsAttack { get; }
        public bool IsDeath { get; }

        public bool UseSkill { get; }
    }
}
