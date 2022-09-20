namespace IM.Analytics.Events
{
    public class GamePauseEvent : AnalyticsEvent
    {
        public override string Key => "game_pause";

        public GamePauseEvent(bool pauseStatus)
        {
            Data.Add("pause_status", pauseStatus);
        }
    }
}