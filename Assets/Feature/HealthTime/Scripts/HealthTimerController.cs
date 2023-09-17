using Jam.HealthTime.Abstracion;
using Jam.UI.GameUI;
using UniRx;
using UnityEngine;

namespace Jam.HealthTime
{
    public class HealthTimerController : MonoBehaviour, IObserveCurrentTime
    {
        [SerializeField] private GameUiView view;
        [SerializeField] private float startTime = 191;

        private readonly ReactiveProperty<float> _currentTime = new ReactiveProperty<float>();

        public IReadOnlyReactiveProperty<float> CurrentTime => _currentTime;

        private void Awake()
        {
            _currentTime.Value = startTime;
            _currentTime.Subscribe(x =>
            {
                view.ShowTime(x, startTime);
            }).AddTo(this);
        }

        private void Update()
        {
            TickTime();
        }

        private void TickTime()
        {
            if (_currentTime.Value <= 0)
            {
                _currentTime.Value = 0;
                return;
            }

            _currentTime.Value -= Time.deltaTime;
        }
    }
}
