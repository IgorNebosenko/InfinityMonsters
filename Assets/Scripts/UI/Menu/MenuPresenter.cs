using System;
using IM.Analytics;
using IM.Analytics.Events;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace IM.UI.Menu
{
    public class MenuPresenter
    {
        private MenuView _view;
        [Inject] private AnalyticsManager _analyticsManager;

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