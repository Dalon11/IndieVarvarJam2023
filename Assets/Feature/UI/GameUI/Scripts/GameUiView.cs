using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace Jam.UI.GameUI
{
    public class GameUiView : MonoBehaviour
    {
        [SerializeField] private Image imageBarHealth;
        [Space]
        [SerializeField] private Image imageIconTime;
        [SerializeField] private Image imageBarTime;
        [SerializeField] private TMP_Text tmpTime;


        public void ShowHealth(float currentValue, float maxValue)
        {
            SetAmount(imageBarHealth, currentValue, maxValue);
        }

        public void ShowTime(float currentValue, float maxValue)
        {
            imageIconTime.enabled = currentValue <= maxValue / 100 * 30;
            SetAmount(imageBarTime, currentValue, maxValue);

            TimeSpan span = TimeSpan.FromSeconds(currentValue);
            tmpTime.text = $"{span.Minutes}:{span.Seconds}";
        }

        private void SetAmount(Image image, float currentValue, float maxValue) => image.fillAmount = currentValue / maxValue;
    }
}
