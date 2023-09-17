using UnityEngine;
using UniRx;
using Jam.HealthTime;
using Jam.Animation;
using Jam.Model;
using UnityEngine.UI;

namespace Jam.Death
{
    public class DeathController : MonoBehaviour
    {
        [SerializeField] private HealthTimerController _observeCurrentTime;
        [SerializeField] private AnimatorProvider animatorProvider;
        [Space]
        [SerializeField] private PlayerModel playerModel;
        [SerializeField] private StateModel stateModel;
        [Space]
        [SerializeField] private Image imageDeath;
        [SerializeField] private Sprite spriteDeath;

        private void Start()
        {
            _observeCurrentTime.CurrentTime.Where(x => x <= 0).Subscribe(_ => Death()).AddTo(this);
            playerModel.Health.Where(x => x <= 0).Subscribe(_ => Death()).AddTo(this);
            stateModel.IsDeath.Value = false;
            animatorProvider.ShowDeath(stateModel.IsDeath.Value);
        }

        private void Death()
        {
            stateModel.IsDeath.Value = true;
            animatorProvider.ShowDeath(stateModel.IsDeath.Value);
            imageDeath.sprite = spriteDeath;
            imageDeath.gameObject.SetActive(true);

        }

    }
}
