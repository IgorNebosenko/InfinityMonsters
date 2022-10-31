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

        private IGameEvents _gameEvents;
        private GooglePlayServicesHandler _playServices;
        private AnalyticsManager _analyticsManager;
 
        //private IGameStats _stats;

        private static readonly List<(int score, AchievementType achievement)> AchievementsScores = new List<(int score, AchievementType achievement)>
        {
            (1000, AchievementType.points1K),
            (5000, AchievementType.points5K),
            (10000, AchievementType.points10K),
            (50000, AchievementType.points50K),
            (100000, AchievementType.points100K)
        };

        public AchievementsHandler(IGameEvents gameEvents, GooglePlayServicesHandler playServices, AnalyticsManager analyticsManager)
        {
            _gameEvents = gameEvents;
            _playServices = playServices;
            _analyticsManager = analyticsManager;

            _gameEvents.OnScoreChanged += OnScoreChanged;
            _gameEvents.OnReset += OnReset;
        }

        ~AchievementsHandler()
        {
            _gameEvents.OnScoreChanged -= OnScoreChanged;
            _gameEvents.OnReset -= OnReset;
        }

        private void OnScoreChanged(int score)
        {
            if (_lastIndex == AchievementsScores.Count)
                return;

            if (score > AchievementsScores[_lastIndex + 1].score)
            {
                ++_lastIndex;
                _analyticsManager.SendEvent(new ReachedAchievementScore(AchievementsScores[_lastIndex].achievement));
                Debug.Log($"[AchievementsHandler] player reached achievement: {AchievementsScores[_lastIndex].achievement}");
                _playServices.SetAchievement(AchievementsScores[_lastIndex].achievement);
            }
        }

        private void OnReset()
        {
            _lastIndex = -1;
        }
    }
}