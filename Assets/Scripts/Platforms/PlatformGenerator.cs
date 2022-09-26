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
        private Coroutine _process;

        public event Action<Vector3> OnPlatformSpawned;
        public float PlatformSize => (distanceBetweenPlatforms - 0.5f) / 2;

        private void Start()
        {
            _lastPositionZ = 0;
            SpawnPlatform();

            _process = StartCoroutine(PlatformSpawnProcess());
        }

        private void OnEnable()
        {
            GameStats.Instance.OnReset += GameReset;
            GameStats.Instance.OnRespawn += PlayerRespawn;
        }
        
        private void OnDisable()
        {
            GameStats.Instance.OnReset -= GameReset;
            GameStats.Instance.OnRespawn -= PlayerRespawn;
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

        private void GameReset()
        {
            StopCoroutine(_process);
            _process = null;
            Start();
        }

        private void PlayerRespawn()
        {
            StopCoroutine(_process);
            _process = null;
            _lastPositionZ -= distanceBetweenPlatforms;
            
            SpawnPlatform();

            _process = StartCoroutine(PlatformSpawnProcess());
        }
    }
}