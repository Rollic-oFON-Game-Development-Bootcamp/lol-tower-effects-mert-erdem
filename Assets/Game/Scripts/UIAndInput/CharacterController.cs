using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Week3_2
{
    public class CharacterController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 1f;
        [SerializeField] private OnScreenJoystick joystick;
        [SerializeField] private Animator characterAnimator;

        private Vector2 joystickDirection => joystick.JoystickDirection;

        private float characterSpeed => moveSpeed * joystickDirection.magnitude;

        // Update is called once per frame
        void Update()
        {
            var worldDirection = new Vector3(joystickDirection.x, 0f, joystickDirection.y);
            transform.position += worldDirection * moveSpeed * Time.deltaTime;

            if (joystickDirection.sqrMagnitude > 0.0001f)
            {
                transform.rotation = Quaternion.LookRotation(worldDirection, Vector3.up);
            }

            characterAnimator.SetFloat("WalkingSpeed", characterSpeed);

            for (int i = 0; i < 100000; i++)
            {
                var foundObject = GameObject.Find("remy");
            }
        }
    }
}
