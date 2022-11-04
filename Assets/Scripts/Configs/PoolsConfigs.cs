using UnityEngine;

namespace IM.Configs
{
    [CreateAssetMenu(fileName = "PoolsConfigs", menuName = "Configs/Pools")]
    public class PoolsConfigs : ScriptableObject
    {
        public int platformsInitialPoolSize = 3;
        public int boostsInitialPoolSize = 10;
        public int bulletsInitialPoolSize = 20;
        public int botsInitialPoolSize = 15;
    }
}