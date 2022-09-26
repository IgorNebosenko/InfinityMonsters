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
        private Vector3 _lastPosition;

        private static readonly Vector3 HeightModifierPlayer = new Vector3(0f, 0.6f, 0f);

        private void Awake()
        {
            _inputFactory = new InputFactory();
            CreatePlayer();
        }

        private void CreatePlayer()
        {
            _player = Instantiate(entitiesConfig.PlayerData.playerEntityPrefab, Vector3.zero + HeightModifierPlayer, 
                Quaternion.identity, transform);
            _player.Init(entitiesConfig.PlayerData.entityData, _inputFactory.GetPlayerInput());
            _player.SetPlayerData(entitiesConfig.PlayerData.projectileData);
        }

        private void OnEnable()
        {
            platformGenerator.OnPlatformSpawned += GenerateBots;

            GameStats.Instance.OnRespawn += PlayerRespawn;
            GameStats.Instance.OnReset += GameRestart;
        }

        private void OnDisable()
        {
            platformGenerator.OnPlatformSpawned += GenerateBots;
            
            GameStats.Instance.OnRespawn -= PlayerRespawn;
            GameStats.Instance.OnReset -= GameRestart;
        }

        private void GenerateBots(Vector3 position)
        {
            _lastPosition = position;
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

        private void PlayerRespawn()
        {
            _player.transform.position = _lastPosition + HeightModifierPlayer;
        }

        private void GameRestart()
        {
            Destroy(_player.gameObject);
            CreatePlayer();
        }
    }
}