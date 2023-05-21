using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Playtipes.InputSystem {
    public class PlayerInput : MonoBehaviour {
        [Header("Input Action References")]
        [SerializeField] private InputActionReference lookAction;
        [SerializeField] private InputActionReference movementAction;
        [SerializeField] private InputActionReference sprintAction;
        [SerializeField] private InputActionReference walkAction;

        private Vector2 movementInput;

        [HideInInspector] public float horizontalInput;
        [HideInInspector] public float verticalInput;



        private void OnEnable() {
            EnableLookInput();
            movementAction.action.Enable();
            sprintAction.action.Enable();
            walkAction.action.Enable();
        }



        private void OnDisable() {
            DisableLookInput();
            movementAction.action.Disable();
            sprintAction.action.Disable();
            walkAction.action.Disable();
        }



        public void HandlePlayerInputs() {
            HandleMovementInput();
        }



        private void HandleMovementInput() {
            movementInput = movementAction.action.ReadValue<Vector2>();

            horizontalInput = movementInput.x;
            verticalInput = movementInput.y;
        }



        public float MoveAmount() {
            if (walkAction.action.IsPressed())
                return 0.25f;

            return Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        }



        private void SetMouseLock(bool _state) {
            if (_state)
                Cursor.lockState = CursorLockMode.Locked;
            else
            if (!_state)
                Cursor.lockState = CursorLockMode.None;
        }



        public void EnableLookInput() {
            lookAction.action.Enable();
            SetMouseLock(true);
        }



        public void DisableLookInput() {
            lookAction.action.Disable();
            SetMouseLock(false);
        }



        public bool IsSprinting() {
            return sprintAction.action.IsPressed() && MoveAmount() >= 0.5f;
        }
    }
}
