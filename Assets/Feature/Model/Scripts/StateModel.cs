using Jam.Model.Abstraction;
using UniRx;
using UnityEngine;

namespace Jam.Model
{
    [CreateAssetMenu(fileName = nameof(StateModel), menuName = "Data/" + nameof(StateModel))]
    public class StateModel : ScriptableObject, IStateModel
    {
        private bool _isWalk;
        private bool _isAttack;

        private bool _useSkill;
        private bool _canWalk = true;
        private ReactiveProperty<bool> isDeath = new ReactiveProperty<bool>();

        public bool IsWalk { get => _isWalk; set => _isWalk = value; }
        public bool IsAttack => _isAttack;

        public bool UseSkill => _useSkill;

        public bool CanWalk { get => _canWalk; set => _canWalk = value; }

        public IReactiveProperty<bool>  IsDeath => isDeath;
    }
}
