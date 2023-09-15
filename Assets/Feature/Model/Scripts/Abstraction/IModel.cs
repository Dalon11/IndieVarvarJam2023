using UniRx;

namespace Jam.Model.Abstraction
{
    public interface IModel
    {
        public IReactiveProperty<float> Health { get; }
        
        public float SpeedMove { get; }

        public float Cooldown { get; }

        public float SpeedAttack { get; }

        public float Damage { get; }
    }
}
