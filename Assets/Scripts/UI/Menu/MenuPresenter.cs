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
        private IAnalyticsManager _analyticsManager;

        public MenuPresenter(IAnalyticsManager analyticsManager, MenuView view)
        {
            _analyticsManager = analyticsManager;
            _view = view;
        }

        public void OnButtonPlayPressed()
        {
            SceneManager.LoadScene(1);
        }

        public void OnButtonNoAdsPressed()
        {
            _analyticsManager.SendEvent(new NoAdsButtonEvent(false)); //ToDo implement it!
            throw new NotImplementedException();
        }

        public void OnButtonAchievementsClicked()
        {
            _analyticsManager.SendEvent(new AchievementsBoardShowEvent());
            Social.ShowAchievementsUI();
        }

        public void OnButtonLeaderBoardClicked()
        {
            _analyticsManager.SendEvent(new LeaderboardShowEvent());
            Social.ShowLeaderboardUI();
        }
    }
}