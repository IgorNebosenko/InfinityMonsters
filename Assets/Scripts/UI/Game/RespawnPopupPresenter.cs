using IM.Ads;
using IM.Analytics;
using IM.Analytics.Events;
using IM.GameData;
using UnityEngine.SceneManagement;
using Zenject;

namespace IM.UI.Game
{
    public class RespawnPopupPresenter
    {
        private RespawnPopup _popup;
        
        [Inject] private AdsManager _adsManager;
        [Inject] private AnalyticsManager _analyticsManager;
        [Inject] private IGameCore _gameCore;
        
        public RespawnPopupPresenter(RespawnPopup popup)
        {
            _popup = popup;
        }

        public void OnShowAdsPressed()
        {
            _analyticsManager.SendEvent(new AfterGameEndFlowEvent(true));
            if (!_adsManager.TryShowRewardedAd(RespawnCallback))
                GameUiReferences.Instance.ErrorShowAdsPopup.gameObject.SetActive(true);

            _popup.gameObject.SetActive(false);
        }

        public void OnRestartPressed()
        {
            _analyticsManager.SendEvent(new AfterGameEndFlowEvent(false));
            _gameCore.RestartGame();
            _popup.gameObject.SetActive(false);
        }

        public void OnToMenuPressed()
        {
            _gameCore.UpdateHighScore();
            SceneManager.LoadScene(0);
        }

        private void RespawnCallback(AdsCallbackStatus status)
        {
            _analyticsManager.SendEvent(new RewardedAdViewEvent(status != AdsCallbackStatus.NotAvailable, status.ToString()));

            switch (status)
            {
                case AdsCallbackStatus.Success:
                    _gameCore.RespawnPlayer();
                    break;
                case AdsCallbackStatus.Skipped:
                    GameUiReferences.Instance.SkippedRewardedAdPopup.gameObject.SetActive(true);
                    break;
                case AdsCallbackStatus.NotAvailable:
                    GameUiReferences.Instance.ErrorShowAdsPopup.gameObject.SetActive(true);
                    break;
            }
            _popup.gameObject.SetActive(false);
        }
    }
}