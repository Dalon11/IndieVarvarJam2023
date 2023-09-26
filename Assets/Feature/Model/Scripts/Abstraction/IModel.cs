using UniRx;

namespace Jam.Model.Abstraction
{
    public interface IModel
    {
        public IReadOnlyReactiveProperty<float> Health { get; }
        
        public float SpeedMove { get; }

        public float ForceRotate { get; }
    }
}
