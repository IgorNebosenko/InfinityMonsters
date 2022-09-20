namespace IM.Analytics.Events
{
    public class AfterGameEndFlowEvent : AnalyticsEvent
    {
        public override string Key => "after_game_end_flow";

        public AfterGameEndFlowEvent(bool restart)
        {
            Data.Add("restart", restart);
        }
    }
}