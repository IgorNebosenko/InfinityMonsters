using System;
using IM.Entity;

namespace IM.Configs
{
    [Serializable]
    public struct PlayerData
    {
        public EntityData entityData;
        public PlayerEntity playerEntityPrefab;
        public ProjectileData projectileData;
    }
}