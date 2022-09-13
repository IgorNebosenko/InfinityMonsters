using UnityEngine;

namespace IM.Entity
{
    public interface IHaveTransform
    {
        Transform EntityTransform { get; }
    }
}