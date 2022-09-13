using UnityEngine;
using Vector2 = System.Numerics.Vector2;

namespace IM.Entity.Input
{
    public interface IInput
    {
        void OnFixedUpdate(Rigidbody rigidbody, float speed);
    }
}