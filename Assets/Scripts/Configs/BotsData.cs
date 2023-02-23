using System;
using IM.Entity;
using UnityEngine;

namespace IM.Configs
{
    [Serializable]
    public struct BotsData
    {
        public EntityData entityData;
        public BotEntity botEntityPrefab;

        public int requiredScore;
        public float weightForSpawn;

        public int minScoreForDestroy;
        public int maxScoreForDestroy;

        public int score;
        public float health;

        [HideInInspector]
        public Vector3 spawnPosition;
    }
}