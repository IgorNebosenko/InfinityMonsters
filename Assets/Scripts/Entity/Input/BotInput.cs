using UnityEngine;

namespace IM.Entity.Input
{
    public class BotInput : IInput
    {
        private IHaveTransform _player;

        public BotInput(IHaveTransform player)
        {
            _player = player;
        }
        
        public void OnFixedUpdate(Rigidbody rigidbody, float speed)
        {
            var transform = rigidbody.transform;
            transform.LookAt(_player.EntityTransform);
            rigidbody.MovePosition(transform.position + transform.forward * speed * Time.fixedDeltaTime);
        }
    }
}