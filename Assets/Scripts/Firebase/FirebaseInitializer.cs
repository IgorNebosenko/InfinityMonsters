using Firebase;
using Firebase.Analytics;
using Firebase.Extensions;
using UnityEngine;

namespace IM.Firebase
{
    public class FirebaseInitializer : MonoBehaviour
    {
        private void Start()
        {
            DontDestroyOnLoad(this);
            FirebaseApp.CheckDependenciesAsync().ContinueWithOnMainThread(task =>
            {
                FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
                Debug.Log("[FirebaseInitializer] analytics enabled!");
            });
        }
    }
}