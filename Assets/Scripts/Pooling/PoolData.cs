using UnityEngine;

namespace IM.Pooling
{
    public class PoolData
    {
        public int minSize;
        public int maxSize;
        public readonly MonoBehaviour monoSample;
        public readonly string name;
        
        public PoolData(int minSize, int maxSize, MonoBehaviour monoSample, string name)
        {
            this.monoSample = monoSample;
            this.name = name;
            this.minSize = minSize;
            this.maxSize = maxSize;
        }
    }
}