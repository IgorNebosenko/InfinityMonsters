using UnityEngine;

namespace IM.Entity.Input
{
    public class BotInput : IInput
    {
        private IHavePosition _player;

        public BotInput(IHavePosition player)
        {
            _player = player;
        }
        
        public void OnFixedUpdate(Rigidbody rigidbody, float speed)
        {
            /*var direction = Vector3.Angle(rigidbody.transform.position, _player.Position);
            
            Debug.Log(direction);*/

            /*direction *= speed * Time.fixedTime;
            rigidbody.MovePosition(rigidbody.transform.position + direction);*/
        }
    }
}