using System.Collections.Generic;
using IM.Analytics.Events;
using UnityEngine;

namespace IM.Analytics
{
    public class AnalyticsManager : MonoBehaviour
    {
        [SerializeField] private bool enableLogs;
        
        private List<IAnalyticsProvider> _analyticsProviders;
        private static AnalyticsManager Instance;

        private void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            
            InitializeAnalyticsProviders();
        }

        private void InitializeAnalyticsProviders()
        {
            _analyticsProviders = new List<IAnalyticsProvider>();
            
            #if UNITY_ANDROID
            _analyticsProviders.Add(new FirebaseAnalyticsProvider());
            #endif

            foreach (var analyticsProvider in _analyticsProviders)
                analyticsProvider.Init(enableLogs);
        }
        
        public static void SendEvent(AnalyticsEvent analyticsEvent)
        {
            foreach (var analyticsProvider in Instance._analyticsProviders)
                analyticsProvider.SendEvent(analyticsEvent);
        }
    }
}