using System;
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
            throw new NotImplementedException();
        }

        public void OnSurrenderPressed()
        {
            AnalyticsManager.SendEvent(new AfterGameEndFlowEvent(false));
            GameStats.Instance.EndGame();
            SceneManager.LoadScene(0);
        }
    }
}