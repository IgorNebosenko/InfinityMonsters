namespace IM.Analytics.Events
{
    public class CollectBoostEvent : AnalyticsEvent
    {
        public override string Key => "collect_boost";

        public CollectBoostEvent(string boostType)
        {
            Data.Add("boost_type", boostType);
        }
    }
}