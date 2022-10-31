using System;
using IM.Ads;
using IM.Analytics;
using IM.Analytics.Events;
using IM.GoogleServices;
using UnityEngine;

namespace IM.GameData
{
    public class GameStats : IHighScoreData, IInGameProperties, IGameEvents, IGameCore
    {
        public const string HighScorePath = "High Score";
        private const int CountGamesBetweenAds = 3;

        private AdsManager _adsManager;
        private GooglePlayServicesHandler _playServices;
        private AnalyticsManager _analyticsManager;

        public int HighScore { get; private set; }
        public int CurrentScore { get; private set; }

        public bool IsGameContinues { get; private set; } = true;
        public bool CanRespawn { get; private set; } = true;

        private static int NumberOfGame = 1;

        private AchievementsHandler _achievementsHandler;

        public event Action<int> OnScoreChanged;
        public event Action<int> OnHighScoreUpdated; 

        public event Action OnRespawn;
        public event Action OnReset;

        public GameStats(AdsManager adsManager, GooglePlayServicesHandler playServices, AnalyticsManager analyticsManager)
        {
            _adsManager = adsManager;
            _playServices = playServices;
            _analyticsManager = analyticsManager;
            
            HighScore = PlayerPrefs.GetInt(HighScorePath, 0);
            _achievementsHandler = new AchievementsHandler(this, playServices, analyticsManager);

            Time.timeScale = 1;
        }

        public void StartGame()
        {
            IsGameContinues = true;
            _analyticsManager.SendEvent(new PlayerStartGameEvent(NumberOfGame));
        }

        public void RespawnPlayer()
        {
            Time.timeScale = 1;
            CanRespawn = false;
            OnRespawn?.Invoke();
            StartGame();
        }

        public void UpdateHighScore()
        {
            _playServices.UpdateHighScore(HighScore);
            if (CurrentScore > HighScore)
            {
                HighScore = CurrentScore;
                PlayerPrefs.SetInt(HighScorePath, HighScore);
                OnHighScoreUpdated?.Invoke(HighScore);
            }
        }

        public void RestartGame()
        {
            UpdateHighScore();
            _analyticsManager.SendEvent(new GameEndEvent(CanRespawn, CurrentScore));
            
            NumberOfGame++;
            Time.timeScale = 1;

            if (NumberOfGame % CountGamesBetweenAds == 0)
                _analyticsManager.SendEvent(new InterstitialAdViewEvent(_adsManager.TryShowInterstitialAd()));
            

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
