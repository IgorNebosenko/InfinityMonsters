namespace IM.Analytics.Events
{
    public class GameEndEvent : AnalyticsEvent
    {
        public override string Key => "game_end";

        public GameEndEvent(bool canRespawn, int score)
        {
            Data.Add("can_respawn", canRespawn);
            Data.Add("score", score);
        }
    }
}