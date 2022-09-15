using System;
using GooglePlayGames;
using UnityEngine;

namespace IM.GoogleServices
{
    public class GooglePlayServicesHandler : MonoBehaviour
    {
        [SerializeField] private bool enableLogs = true;
        public static GooglePlayServicesHandler Instance { get; private set; }
        
        private void Awake()
        {
            DontDestroyOnLoad(this);
            Instance = this;
            
            PlayGamesPlatform.DebugLogEnabled = true;
            PlayGamesPlatform.Activate();
            Social.localUser.Authenticate(result =>
            {
                if (enableLogs)
                    Debug.Log($"[GooglePlayServicesHandler] auth status success: {result}");
            });
        }

        public void UpdateHighScore(int value)
        {
            Social.ReportProgress(Constants.leaderboard_highscores_leaderboard, value, result =>
            {
                if (enableLogs)
                    Debug.Log($"[GooglePlayServicesHandler] report high score status success: {result}");
            });
        }

        public void SetAchievement(AchievementType type)
        {
            throw new NotImplementedException();
            switch (type)
            {
                case AchievementType.points1K:
                    break;
                case AchievementType.points5K:
                    break;
                case AchievementType.points10K:
                    break;
            }
        }
    }
}
