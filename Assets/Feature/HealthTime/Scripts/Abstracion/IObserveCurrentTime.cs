 using UniRx; 

namespace Jam.HealthTime.Abstracion
{
    public interface IObserveCurrentTime  
    {
        public IReadOnlyReactiveProperty<float> CurrentTime { get; }
    }
}
