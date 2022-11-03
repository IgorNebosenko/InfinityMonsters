using System;
using System.Collections;
using System.Collections.Generic;
using IM.GameData;
using UnityEngine;
using Zenject;

namespace IM.Platforms
{
    public class PlatformGenerator : MonoBehaviour
    {
        [Inject] private PlatformToken.Factory _factory;
        private List<PlatformToken> _tokens = new List<PlatformToken>();

        [SerializeField] private float platformSpawnTime = 5f;
        [SerializeField] private float distanceBetweenPlatforms = 10f;

        private float _lastPositionZ;
        private Coroutine _process;

        [Inject] private IGameEvents _gameEvents;
        [Inject] private IInGameProperties _gameProperties;

        private PlatformToken GetPlatformToken(Vector3 pos)
        {
            var token = _factory.Create(pos);
            if (!_tokens.Contains(token))
                _tokens.Add(token);

            return token;
        }

        private void DisposeTokens()
        {
            for (var i = 0; i < _tokens.Count; i++)
            {
                _tokens[i]?.Dispose();
            }
        }

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
            _gameEvents.OnReset += GameReset;
            _gameEvents.OnRespawn += PlayerRespawn;
        }
        
        private void OnDisable()
        {
            _gameEvents.OnReset -= GameReset;
            _gameEvents.OnRespawn -= PlayerRespawn;
        }

        private void SpawnPlatform()
        {
            var pos = Vector3.forward * _lastPositionZ;
            GetPlatformToken(pos);
            OnPlatformSpawned?.Invoke(pos);
            _lastPositionZ += distanceBetweenPlatforms;
        }

        private IEnumerator PlatformSpawnProcess()
        {
            while (_gameProperties.IsGameContinues)
            {
                yield return new WaitForSeconds(platformSpawnTime);
                SpawnPlatform();
            }
        }

        private void GameReset()
        {
            StopCoroutine(_process);
            _process = null;
            DisposeTokens();
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