using IM.GameData;
using UnityEngine;

namespace IM.Platforms
{
    public class Platform : MonoBehaviour
    {
        [SerializeField] private float platformLiveTime = 10f;

        [SerializeField] private GameObject platform;

        private bool _isUpdateInterrupted;

        private void OnEnable()
        {
            GameStats.Instance.OnRespawn += ForceDestroy;
            GameStats.Instance.OnReset += ForceDestroy;
        }

        private void OnDisable()
        {
            GameStats.Instance.OnRespawn -= ForceDestroy;
            GameStats.Instance.OnReset -= ForceDestroy;
        }

        private void Update()
        {
            if (_isUpdateInterrupted)
                return;
            
            platformLiveTime -= Time.deltaTime;
            //TODO: display it time near to platform

            if (platformLiveTime <= 0)
            {
                ForceDestroy();
            }
        }

        private void ForceDestroy()
        {
            Destroy(platform);
            _isUpdateInterrupted = true;
        }
    }
}