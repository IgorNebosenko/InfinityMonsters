using System;
using IM.Ads;
using IM.Analytics;
using IM.Analytics.Events;
using IM.GameData;
using UnityEngine.SceneManagement;

namespace IM.UI.Game
{
    public class RespawnPopupPresenter
    {
        private RespawnPopup _popup;
        public RespawnPopupPresenter(RespawnPopup popup)
        {
            _popup = popup;
        }

        public void OnShowAdsPressed()
        {
            AnalyticsManager.SendEvent(new AfterGameEndFlowEvent(true));
            AdsManager.TryShowRewardedAd(RespawnCallback);
            _popup.gameObject.SetActive(false);
        }

        public void OnRestartPressed()
        {
            AnalyticsManager.SendEvent(new AfterGameEndFlowEvent(false));
            GameStats.Instance.RestartGame();
            _popup.gameObject.SetActive(false);
        }

        public void OnToMenuPressed()
        {
            SceneManager.LoadScene(0);
        }

        private void RespawnCallback(AdsCallbackStatus status)
        {
            AnalyticsManager.SendEvent(new RewardedAdViewEvent(status != AdsCallbackStatus.NotAvailable, status.ToString()));

            switch (status)
            {
                case AdsCallbackStatus.Success:
                    GameStats.Instance.RespawnPlayer();
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