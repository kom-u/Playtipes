using UnityEngine;
using Playtipes.Core;
using UnityEngine.InputSystem;

namespace Playtipes.InputSystem {
    public class PlayerInput : MonoBehaviour {
        [Header("Input Action References")]
        [SerializeField] private InputActionReference lookAction;
        [SerializeField] private InputActionReference movementAction;
        [SerializeField] private InputActionReference sprintAction;
        [SerializeField] private InputActionReference walkAction;
        [SerializeField] private InputActionReference interactAction;

        private Vector2 movementInput;

        [HideInInspector] public float horizontalInput;
        [HideInInspector] public float verticalInput;



        private void OnEnable() {
            GameManager.Instance.gameEvent.OnStart += GameManager_GameEvent_OnStart;
            GameManager.Instance.gameEvent.OnObjectiveCompleted += GameManager_GameEvent_OnObjectiveCompleted;
        }



        private void OnDisable() {
            GameManager.Instance.gameEvent.OnStart -= GameManager_GameEvent_OnStart;
            GameManager.Instance.gameEvent.OnObjectiveCompleted -= GameManager_GameEvent_OnObjectiveCompleted;
        }



        private void GameManager_GameEvent_OnStart(GameEvent _gameEvent) {
            EnableLookInput();
            movementAction.action.Enable();
            sprintAction.action.Enable();
            walkAction.action.Enable();
            interactAction.action.Enable();
        }



        private void GameManager_GameEvent_OnObjectiveCompleted(GameEvent _gameEvent) {
            DisableLookInput();
            movementAction.action.Disable();
            sprintAction.action.Disable();
            walkAction.action.Disable();
            interactAction.action.Disable();
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



        public bool IsInteracting() {
            return interactAction.action.IsPressed();
        }



        public void ResetInputValue() {
            horizontalInput = 0;
            verticalInput = 0;
        }
    }
}
