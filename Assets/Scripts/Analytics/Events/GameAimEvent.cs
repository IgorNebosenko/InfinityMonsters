namespace IM.Analytics.Events
{
    public class GameAimEvent : AnalyticsEvent
    {
        public override string Key => "game_aim";

        public GameAimEvent(bool aimStatus)
        {
            Data.Add("aim_status", aimStatus);
        }
    }
}