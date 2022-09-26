using UnityEngine;
using UnityEngine.UI;

namespace IM.UI.Game
{
    public class SkippedRewardedAdPopup : MonoBehaviour
    {
        [SerializeField] private Button restartButton;
        [SerializeField] private Button toMenuButton;

        private SkippedRewardedAdPresenter _presenter;

        private void Awake()
        {
            _presenter = new SkippedRewardedAdPresenter(this);
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
