using IM.GameData;
using UnityEngine;
using UnityEngine.UI;

namespace IM.UI.Game
{
    public class RespawnPopup : MonoBehaviour
    {
        [SerializeField] private GameObject showAdsButtonObject;
        [SerializeField] private Button showAdsButton;
        [SerializeField] private Button restartButton;
        [SerializeField] private Button toMenuButton;

        private RespawnPopupPresenter _presenter;

        private void Awake()
        {
            _presenter = new RespawnPopupPresenter(this);
        }

        private void OnEnable()
        {
            showAdsButtonObject.SetActive(GameStats.Instance.CanRespawn);
            showAdsButton.onClick.AddListener(_presenter.OnShowAdsPressed);
            restartButton.onClick.AddListener(_presenter.OnRestartPressed);
            toMenuButton.onClick.AddListener(_presenter.OnToMenuPressed);
        }

        public void OnDisable()
        {
            showAdsButton.onClick.RemoveListener(_presenter.OnShowAdsPressed);
            restartButton.onClick.RemoveListener(_presenter.OnRestartPressed);
            toMenuButton.onClick.RemoveListener(_presenter.OnToMenuPressed);
        }
    }
}