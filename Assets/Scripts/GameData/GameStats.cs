using System;
using IM.Ads;
using IM.Analytics;
using IM.Analytics.Events;
using IM.GoogleServices;
using UnityEngine;

namespace IM.GameData
{
    public class GameStats : MonoBehaviour
    {
        public const string HighScorePath = "High Score";
        private const int CountGamesBetweenAds = 3;
        
        public int HighScore { get; private set; }
        public int CurrentScore { get; private set; }

        public bool IsGameContinues { get; private set; } = true;
        public bool CanRespawn { get; private set; } = true;

        public static int NumberOfGame = 1;

        public static GameStats Instance { get; private set; }

        private AchievementsHandler _achievementsHandler;

        public event Action<int> OnScoreChanged;
        
        public event Action OnRespawn;
        public event Action OnReset;

        private void Awake()
        {
            Instance = this;
            HighScore = PlayerPrefs.GetInt(HighScorePath, 0);
            _achievementsHandler = new AchievementsHandler(this);

            Time.timeScale = 1;
        }

        public void StartGame()
        {
            IsGameContinues = true;
            AnalyticsManager.SendEvent(new PlayerStartGameEvent(NumberOfGame));
        }

        public void RespawnPlayer()
        {
            Time.timeScale = 1;
            CanRespawn = false;
            OnRespawn?.Invoke();
            StartGame();
        }

        public void RestartGame()
        {
            if (CurrentScore > HighScore)
            {
                HighScore = CurrentScore;
                PlayerPrefs.SetInt(HighScorePath, HighScore);
                GooglePlayServicesHandler.Instance.UpdateHighScore(HighScore);
            }
            AnalyticsManager.SendEvent(new GameEndEvent(CanRespawn, CurrentScore));
            
            NumberOfGame++;
            Time.timeScale = 1;

            if (NumberOfGame % CountGamesBetweenAds == 0)
            {
                AnalyticsManager.SendEvent(new InterstitialAdViewEvent(AdsManager.TryShowInterstitialAd()));
            }

            CurrentScore = 0;
            OnScoreChanged?.Invoke(0);
            CanRespawn = true;
            
            OnReset?.Invoke();
            StartGame();
        }

        public void UpdateCurrentScore(int val)
        {
            if (val <= 0)
                return;
            
            CurrentScore += val;

            OnScoreChanged?.Invoke(CurrentScore);
        }
    }
}
