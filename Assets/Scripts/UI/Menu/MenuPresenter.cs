using System;
using IM.Analytics;
using IM.Analytics.Events;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace IM.UI.Menu
{
    public class MenuPresenter
    {
        private MenuView _view;

        public MenuPresenter(MenuView view)
        {
            _view = view;
        }

        public void OnButtonPlayPressed()
        {
            SceneManager.LoadScene(1);
        }

        public void OnButtonNoAdsPressed()
        {
            AnalyticsManager.SendEvent(new NoAdsButtonEvent(false)); //ToDo implement it!
            throw new NotImplementedException();
        }

        public void OnButtonAchievementsClicked()
        {
            AnalyticsManager.SendEvent(new AchievementsBoardShowEvent());
            Social.ShowAchievementsUI();
        }

        public void OnButtonLeaderBoardClicked()
        {
            AnalyticsManager.SendEvent(new LeaderboardShowEvent());
            Social.ShowLeaderboardUI();
        }
    }
}