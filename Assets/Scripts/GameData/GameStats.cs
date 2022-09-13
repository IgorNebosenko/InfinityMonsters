using System;
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

        public static GameStats Instance { get; private set; }

        public event Action<int> OnScoreChanged;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;

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

        public void EndGame()
        {
            if (CurrentScore > HighScore)
                PlayerPrefs.SetInt(HighScorePath, HighScore);

            IsGameContinues = false;
        }

        public void RestartGame()
        {
            CurrentScore = 0;

            IsGameContinues = true;
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
