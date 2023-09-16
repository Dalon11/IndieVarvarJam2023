using Jam.Model.Abstraction;
using UnityEngine;

namespace Jam.Model
{
    [CreateAssetMenu(fileName = nameof(StateModel), menuName = "Data/" + nameof(StateModel))]
    public class StateModel : ScriptableObject, IStateModel
    {
        private bool _isWalk;
        private bool _isAttack;
        private bool _isDeath;

        private bool _useSkill;

        public bool IsWalk { get => _isWalk; set => _isWalk = value; }

        public bool IsAttack => _isAttack;
        public bool IsDeath => _isDeath;

        public bool UseSkill => _useSkill;


    }
}
