using System.Collections.Generic;
using IM.Analytics;
using IM.Analytics.Events;
using IM.GoogleServices;
using UnityEngine;

namespace IM.GameData
{
    public class AchievementsHandler
    {
        private int _lastIndex = -1;
        private GameStats _stats;

        private static readonly List<(int score, AchievementType achievement)> AchievementsScores = new List<(int score, AchievementType achievement)>
        {
            (1000, AchievementType.points1K),
            (5000, AchievementType.points5K),
            (10000, AchievementType.points10K),
            (50000, AchievementType.points50K),
            (100000, AchievementType.points100K)
        };

        public AchievementsHandler(GameStats stats)
        {
            _stats = stats;

            _stats.OnScoreChanged += OnScoreChanged;
            _stats.OnReset += OnReset;
        }

        ~AchievementsHandler()
        {
            _stats.OnScoreChanged -= OnScoreChanged;
            _stats.OnReset -= OnReset;
        }

        private void OnScoreChanged(int score)
        {
            if (_lastIndex == AchievementsScores.Count)
                return;

            if (score > AchievementsScores[_lastIndex + 1].score)
            {
                ++_lastIndex;
                AnalyticsManager.SendEvent(new ReachedAchievementScore(AchievementsScores[_lastIndex].achievement));
                Debug.Log($"[AchievementsHandler] player reached achievement: {AchievementsScores[_lastIndex].achievement}");
                GooglePlayServicesHandler.Instance.SetAchievement(AchievementsScores[_lastIndex].achievement);
            }
        }

        private void OnReset()
        {
            _lastIndex = -1;
        }
    }
}