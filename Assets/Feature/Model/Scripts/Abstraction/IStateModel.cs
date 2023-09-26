
using UniRx;

namespace Jam.Model.Abstraction
{
    public interface IStateModel
    {
        public bool CanWalk { get; set; }
        public bool IsWalk { get; set; }
        public bool IsAttack { get; }
        public bool UseSkill { get; }

        public IReactiveProperty<bool> IsDeath { get; }
    }
}
