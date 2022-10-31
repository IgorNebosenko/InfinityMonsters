using System;
using System.Collections.Generic;
using IM.Configs;
using IM.GameData;
using IM.Platforms;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace IM.Boosts
{
    public class BoostGenerator : MonoBehaviour
    {
        [SerializeField] private PlatformGenerator platformGenerator;
        [SerializeField] private BoostConfig boostConfig;

        private const float BoostHeight = 0.65f;
        private List<BoostBase> _listNewBoosts;
        private List<BoostBase> _listOldBoosts;

        [Inject] private IGameEvents _gameEvents;
        [Inject] private IHighScoreData _scoreData;

        private void Awake()
        {
            _listNewBoosts = new List<BoostBase>();
            _listOldBoosts = new List<BoostBase>();
        }

        private void OnEnable()
        {
            platformGenerator.OnPlatformSpawned += SpawnBoosts;
            _gameEvents.OnReset += DestroyAllBoosts;
        }

        private void OnDisable()
        {
            platformGenerator.OnPlatformSpawned -= SpawnBoosts;
            _gameEvents.OnReset -= DestroyAllBoosts;
        }

        private void SpawnBoosts(Vector3 position)
        {
            foreach (var child in _listOldBoosts)
            {
                if (child != null)
                    Destroy(child.gameObject);
            }
            _listOldBoosts.Clear();
            _listOldBoosts.AddRange(_listNewBoosts);
            _listNewBoosts.Clear();

            var listBoostsGen = boostConfig.GetListBoosts(_scoreData.CurrentScore);

            for (var i = 0; i < listBoostsGen.Count; i++)
            {
                var pos = position +
                          Vector3.right * GetRandomPosition() +
                          Vector3.up * BoostHeight +
                          Vector3.forward * GetRandomPosition();

                _listNewBoosts.Add(Instantiate(listBoostsGen[i], pos, Quaternion.identity, transform));
            }
        }

        private float GetRandomPosition()
        {
            return Random.Range(platformGenerator.PlatformSize * -1, platformGenerator.PlatformSize);
        }

        private void DestroyAllBoosts()
        {
            foreach (var child in _listOldBoosts)
            {
                if (child != null)
                    Destroy(child.gameObject);
            }
            _listOldBoosts.Clear();
            _listNewBoosts.Clear();
        }
    }
}