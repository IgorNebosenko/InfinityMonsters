using IM.Analytics.Events;

namespace IM.Analytics
{
    public interface IAnalyticsManager
    {
        void SendEvent(AnalyticsEvent analyticsEvent);
    }
}