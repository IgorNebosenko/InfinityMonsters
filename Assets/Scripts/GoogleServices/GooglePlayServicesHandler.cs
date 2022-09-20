using System;
using System.Collections.Generic;
using GooglePlayGames;
using IM.Analytics;
using IM.Analytics.Events;
using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace IM.GoogleServices
{
    public class GooglePlayServicesHandler : MonoBehaviour
    {
        [SerializeField] private bool enableLogs = true;

        private List<IAchievement> _achievements;
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
                Social.LoadAchievements(OnLoadAchievements);
            });
        }

        private void OnLoadAchievements(IAchievement[] list)
        {
            _achievements = new List<IAchievement>(list);
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
            if (_achievements == null || _achievements.Count == 0)
            {
                if (enableLogs)
                    Debug.LogWarning("[GooglePlayServicesHandler] Can't set achievement, list achievements is empty!");
                return;
            }
            
            IAchievement achievement = null;

            switch (type)
            {
                case AchievementType.points1K:
                    achievement = _achievements.Find(x => string.Equals(x.id, Constants.achievement_reach_1000_points));
                    break;
                case AchievementType.points5K:
                    achievement = _achievements.Find(x => string.Equals(x.id, Constants.achievement_reach_5000_points));
                    break;
                case AchievementType.points10K:
                    achievement = _achievements.Find(x => string.Equals(x.id, Constants.achievement_reach_10_000_points));
                    break;
            }

            if (achievement == null)
            {
                if (enableLogs)
                    Debug.LogWarning($"[GooglePlayServicesHandler] can't find achievement for type {type}!");
                return;
            }
            
            if (achievement.completed)
                return;

            achievement.percentCompleted = 100;
            AnalyticsManager.SendEvent(new ReachedAchievementScore(type));
        }
    }
}
