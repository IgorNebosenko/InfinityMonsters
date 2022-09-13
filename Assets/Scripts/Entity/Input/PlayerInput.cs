using IM.UI.Game;
using UnityEngine;

namespace IM.Entity.Input
{
    public class PlayerInput : IInput
    {
        private Joystick _joystick;

        public PlayerInput()
        {
            _joystick = GameUiReferences.Instance.Joystick;
        }

        public void OnFixedUpdate(Rigidbody rigidbody, float speed)
        {
            var direction = Vector3.forward * _joystick.Vertical +
                            Vector3.right * _joystick.Horizontal;

            if (_joystick.Direction != Vector2.zero) // rotation player
            {
                var angle = Vector2.Angle(Vector2.up, _joystick.Direction);
                if (_joystick.Horizontal < 0)
                    angle *= -1;

                rigidbody.transform.eulerAngles = Vector3.up * angle;
            }

            direction *= speed * Time.fixedDeltaTime;
            rigidbody.MovePosition(rigidbody.transform.position + direction);
        }
    }
}