using System;
using IM.Configs;
using IM.GameData;
using IM.Platforms;
using UnityEngine;
using Random = UnityEngine.Random;

namespace IM.Entity
{
    public class EntityGenerator : MonoBehaviour
    {
        [SerializeField] private EntitiesConfig entitiesConfig;
        [SerializeField] private PlatformGenerator platformGenerator;

        private InputFactory _inputFactory;

        private PlayerEntity _player;

        private void Awake()
        {
            _inputFactory = new InputFactory();
            _player = Instantiate(entitiesConfig.PlayerData.playerEntityPrefab, new Vector3(0, 0.6f, 0),
                Quaternion.identity, transform);
            _player.Init(entitiesConfig.PlayerData.entityData, _inputFactory.GetPlayerInput());
            _player.SetPlayerData(entitiesConfig.PlayerData.projectileData);
        }

        private void OnEnable()
        {
            platformGenerator.OnPlatformSpawned += GenerateBots;
        }

        private void OnDisable()
        {
            platformGenerator.OnPlatformSpawned += GenerateBots;
        }

        private void GenerateBots(Vector3 position)
        {
            var config = entitiesConfig.GetBotsDataForSpawn(GameStats.Instance.CurrentScore);

            for (var i = 0; i < config.Length; i++)
            {
                var cfg = config[i];
                
                var bot = Instantiate(cfg.botEntityPrefab, position + cfg.spawnPosition, GetRandomEntityRotation(), transform);
                bot.Init(config[i].entityData, _inputFactory.GetBotInput(_player));
                bot.SetScoreForDestroy(config[i].minScoreForDestroy, config[i].maxScoreForDestroy);
            }
        }

        private Quaternion GetRandomEntityRotation()
        {
            return new Quaternion(0, Random.Range(-1f, 1f), 0, 1);
        }
    }
}