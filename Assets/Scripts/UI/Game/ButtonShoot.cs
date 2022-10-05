using System;
using IM.Analytics;
using IM.Analytics.Events;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace IM.UI.Game
{
    public class ButtonShoot : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private Image imageAimStatus;
        [Space] 
        [SerializeField] private Sprite onAimEnabledIcon;
        [SerializeField] private Sprite onAimDisabledIcon;

        private bool _isHolded;

        public event Action OnHoldButton;

        private void Awake()
        {
            _isHolded = false;
            SetButtonState();
        }

        private void OnEnable()
        {
            button.onClick.AddListener(ButtonHoldStatusChange);
        }

        private void OnDisable()
        {
            button.onClick.RemoveListener(ButtonHoldStatusChange);
        }

        private void LateUpdate()
        {
            if (_isHolded)
                OnHoldButton?.Invoke();
        }

        private void ButtonHoldStatusChange()
        {
            _isHolded = !_isHolded;
            AnalyticsManager.SendEvent(new GameAimEvent(_isHolded));
            SetButtonState();
        }

        private void SetButtonState()
        {
            imageAimStatus.sprite = _isHolded ? onAimEnabledIcon : onAimDisabledIcon;
        }

    }
}