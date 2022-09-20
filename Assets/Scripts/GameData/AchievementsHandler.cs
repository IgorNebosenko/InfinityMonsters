using System.Collections.Generic;
using IM.Analytics;
using IM.Analytics.Events;
using IM.GoogleServices;
using UnityEngine;

namespace IM.GameData
{
    public class AchievementsHandler
    {
        private int _lastScore;
        private GameStats _stats;

        private static readonly List<(int score, AchievementType achievement)> AchievementsScores = new List<(int score, AchievementType achievement)>
        {
            (1000, AchievementType.points1K),
            (5000, AchievementType.points5K),
            (10000, AchievementType.points10K)
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
            var index = -1;
            
            for (var i = 0; i < AchievementsScores.Count; i++)
            {
                if (AchievementsScores[i].score <= score)
                    index = i;
                else
                    break;
            }

            if (index >= 0 && AchievementsScores[index].score > _lastScore)
            {
                AnalyticsManager.SendEvent(new ReachedAchievementScore(AchievementsScores[index].achievement));
                Debug.Log($"[AchievementsHandler] player reached achievement: {AchievementsScores[index].achievement}");
            }

            _lastScore = score;
        }

        private void OnReset()
        {
            _lastScore = 0;
        }
    }
}