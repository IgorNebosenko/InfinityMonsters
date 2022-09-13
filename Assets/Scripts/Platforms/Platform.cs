using UnityEngine;

namespace IM.Platforms
{
    public class Platform : MonoBehaviour
    {
        [SerializeField] private float platformLiveTime = 10f;

        [SerializeField] private GameObject platform;

        private bool _isUpdateInterrupted;

        private void Update()
        {
            if (_isUpdateInterrupted)
                return;
            
            platformLiveTime -= Time.deltaTime;
            //TODO: display it time near to platform

            if (platformLiveTime <= 0)
            {
                Destroy(platform);
                _isUpdateInterrupted = true;
            }
        }
    }
}