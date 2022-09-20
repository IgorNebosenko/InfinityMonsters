using System;
using IM.Analytics;
using IM.Analytics.Events;
using IM.GoogleServices;
using UnityEngine;

namespace IM.GameData
{
    public class GameStats : MonoBehaviour
    {
        public const string HighScorePath = "High Score";
        
        public int HighScore { get; private set; }
        public int CurrentScore { get; private set; }

        public bool IsGameContinues { get; private set; } = true;
        public bool CanRespawn { get; private set; } = true;

        public static int NumberOfGame;

        public static GameStats Instance { get; private set; }

        private AchievementsHandler _achievementsHandler;

        public event Action<int> OnScoreChanged;
        public event Action OnReset;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;

            _achievementsHandler = new AchievementsHandler(this);

            Time.timeScale = 1;
        }

        private void Start()
        {
            HighScore = PlayerPrefs.GetInt(HighScorePath, 0);
        }

        private void OnDestroy()
        {
            Instance = null;
        }

        public void StartGame()
        {
            CurrentScore = 0;

            IsGameContinues = true;
            AnalyticsManager.SendEvent(new PlayerStartGameEvent(NumberOfGame));
        }

        public void EndGame()
        {
            if (CurrentScore > HighScore)
            {
                HighScore = CurrentScore;
                PlayerPrefs.SetInt(HighScorePath, HighScore);
                GooglePlayServicesHandler.Instance.UpdateHighScore(HighScore);
            }
            AnalyticsManager.SendEvent(new GameEndEvent(CanRespawn, CurrentScore));

            IsGameContinues = false;
            NumberOfGame++;
        }

        public void RestartGame()
        {
            NumberOfGame++;
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
