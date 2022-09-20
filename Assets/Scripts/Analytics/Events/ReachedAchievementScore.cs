using IM.GoogleServices;

namespace IM.Analytics.Events
{
    public class ReachedAchievementScore : AnalyticsEvent
    {
        public override string Key => "reached_achievement_score";

        public ReachedAchievementScore(AchievementType achievementType)
        {
            Data.Add("achievement_type", achievementType.ToString());
        }
    }
}