using System;
using IM.Analytics;
using IM.Analytics.Events;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace IM.UI.Menu
{
    public class MenuPresenter
    {
        private const string soundState = "sound_state";
        private const string musicState = "music_state";

        public bool IsSoundPlay { get; private set; }
        public bool IsMusicPlay { get; private set; }

        public MenuPresenter()
        {
            SceneManager.LoadScene(1);
            IsSoundPlay = PlayerPrefs.GetInt(soundState, 1) == 0 ? false : true;
            IsMusicPlay = PlayerPrefs.GetInt(musicState, 1) == 0 ? false : true;
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

       
        public void ChangeMusicState(Action<bool> callback)
        {
            IsMusicPlay = !IsMusicPlay;
            PlayerPrefs.SetInt(soundState, IsMusicPlay ? 1 : 0);
            callback?.Invoke(IsMusicPlay);
            
        }
        public void ChangeSoundState(Action<bool> callback)
        {
            IsSoundPlay = !IsSoundPlay;
            PlayerPrefs.SetInt(soundState, IsSoundPlay ? 1 : 0);
            callback?.Invoke(IsSoundPlay);
        }
    }
}