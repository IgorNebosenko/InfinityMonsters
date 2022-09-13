using System;
using System.Collections;
using IM.Configs;
using IM.GameData;
using UnityEngine;

namespace IM.Platforms
{
    public class PlatformGenerator : MonoBehaviour
    {
        [SerializeField] private float platformSpawnTime = 8f;
        [SerializeField] private float distanceBetweenPlatforms = 5f;
        [SerializeField] private Platform templateInstance;

        private float _lastPositionZ;

        public event Action<Vector3> OnPlatformSpawned;
        public float PlatformSize => (distanceBetweenPlatforms - 0.5f) / 2;

        private void Start()
        {
            SpawnPlatform();

            StartCoroutine(PlatformSpawnProcess());
        }

        private void SpawnPlatform()
        {
            var pos = Vector3.forward * _lastPositionZ;
            Instantiate(templateInstance, pos, Quaternion.identity, transform);
            OnPlatformSpawned?.Invoke(pos);
            _lastPositionZ += distanceBetweenPlatforms;
        }

        private IEnumerator PlatformSpawnProcess()
        {
            while (GameStats.Instance.IsGameContinues)
            {
                yield return new WaitForSeconds(platformSpawnTime);
                SpawnPlatform();
            }
        }
    }
}