using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Week3_1
{
    public class CharacterController : MonoBehaviour
    {
        [SerializeField, BoxGroup("Setup")] private Transform sideMovementRoot;
        [SerializeField, BoxGroup("Setup")] private Transform leftLimit, rightLimit;

        [SerializeField, BoxGroup("Settings")] private float characterSpeed = 5f, sideMovementSensitivity = 1f, rotationSpeed = 1f;
        [SerializeField, BoxGroup("Settings")] private float sideMovementLerpSpeed = 20f;

        private Vector2 inputDrag;

        private Vector2 previousMousePosition;

        private float leftLimitX => leftLimit.localPosition.x;
        private float rightLimitX => rightLimit.localPosition.x;

        private float sideMovementTarget = 0f;

        private Vector2 mousePositionCM
        {
            get
            {
                Vector2 pixels = Input.mousePosition;
                var inches = pixels / Screen.dpi;
                var centimeters = inches * 2.54f;

                return centimeters;
            }
        }

        // Update is called once per frame
        void Update()
        {
            HandleInput();
            HandleForwardMovement();
            HandleSideMovement();
        }

        private void HandleForwardMovement()
        {
            transform.position += transform.forward * Time.deltaTime * characterSpeed;
        }

        private void HandleSideMovement()
        {
            sideMovementTarget += inputDrag.x * sideMovementSensitivity;
            sideMovementTarget = Mathf.Clamp(sideMovementTarget, leftLimitX, rightLimitX);

            var localPos = sideMovementRoot.localPosition;

            localPos.x = Mathf.Lerp(localPos.x, sideMovementTarget, Time.deltaTime * sideMovementLerpSpeed);

            sideMovementRoot.localPosition = localPos;

            //var moveDirection = Vector3.forward * 0.5f;
            //moveDirection += sideMovementRoot.right * inputDrag.x * sideMovementSensitivity;
            //
            //moveDirection.Normalize();
            //
            //var targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            //
            //sideMovementRoot.rotation = Quaternion.Lerp(sideMovementRoot.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }

        private void HandleInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                previousMousePosition = mousePositionCM;
            }
            if (Input.GetMouseButton(0))
            {
                var deltaMouse = mousePositionCM - previousMousePosition;
                inputDrag = deltaMouse;
                previousMousePosition = mousePositionCM;
            }
            else
            {
                inputDrag = Vector2.zero;
            }
        }
    }
}

