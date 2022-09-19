namespace IM.Analytics.Events
{
    public class PlayerStartGameEvent : AnalyticsEvent
    {
        public override string Key => "player_start_game";

        public PlayerStartGameEvent(int numberAtSession)
        {
            Data.Add("numberAtSession", numberAtSession);
        }
    }
}