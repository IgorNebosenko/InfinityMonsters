using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IM
{
    public interface IHaveHealth
    {
        float CurrentHealth { get; }

        public void SetHealth(float healhCount);
        public void DoDamage(float damage);
    }
}
