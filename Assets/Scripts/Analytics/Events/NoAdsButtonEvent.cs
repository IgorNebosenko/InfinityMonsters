namespace IM.Analytics.Events
{
    public class NoAdsButtonEvent : AnalyticsEvent
    {
        public override string Key => "press_no_ads";

        public NoAdsButtonEvent(bool tryBuy)
        {
            Data.Add("try_buy", tryBuy);
        }
    }
}