using System.Collections.Generic;
using IM.Analytics.Events;
using UnityEngine;

namespace IM.Analytics
{
    public class AnalyticsManager : IAnalyticsManager
    {
        private bool _enableLogs;
        
        private List<IAnalyticsProvider> _analyticsProviders;

        public AnalyticsManager(bool enableLogs)
        {
            _enableLogs = enableLogs;
            _analyticsProviders = new List<IAnalyticsProvider>();
            
#if UNITY_ANDROID
            _analyticsProviders.Add(new FirebaseAnalyticsProvider());
#endif

            foreach (var analyticsProvider in _analyticsProviders)
                analyticsProvider.Init(_enableLogs);
        }

        public void SendEvent(AnalyticsEvent analyticsEvent)
        {
            if (_analyticsProviders == null)
            {
                if (_enableLogs)
                    Debug.LogWarning($"[AnalyticsManager] no analytics providers in list for send event {analyticsEvent}");
                return;
            }
            
            foreach (var analyticsProvider in _analyticsProviders)
                analyticsProvider.SendEvent(analyticsEvent);
        }
    }
}