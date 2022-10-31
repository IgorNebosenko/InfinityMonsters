namespace IM.GoogleServices
{
    public interface IGooglePlayGameServices
    {
        void UpdateHighScore(int value);
        void SetAchievement(AchievementType type);
    }
}