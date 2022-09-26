using UnityEngine;
using UnityEngine.UI;

namespace IM.UI.Game
{
    public class ErrorShowAdsPopup : MonoBehaviour
    {
        [SerializeField] private Button restartButton;
        [SerializeField] private Button toMenuButton;

        private ErrorShowAdsPresenter _presenter;

        private void Awake()
        {
            _presenter = new ErrorShowAdsPresenter(this);
        }

        private void OnEnable()
        {
            restartButton.onClick.AddListener(_presenter.OnRestartButtonPressed);
            toMenuButton.onClick.AddListener(_presenter.OnToMenuButtonPressed);
        }

        private void OnDisable()
        {
            restartButton.onClick.RemoveListener(_presenter.OnRestartButtonPressed);
            toMenuButton.onClick.RemoveListener(_presenter.OnToMenuButtonPressed);
        }
    }
}
