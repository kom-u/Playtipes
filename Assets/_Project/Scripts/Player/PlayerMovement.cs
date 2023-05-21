using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Playtipes.InputSystem;

namespace Playtipes.Player {
    [DisallowMultipleComponent]
    #region RequireComponents
    [RequireComponent(typeof(PlayerAnimator))]
    [RequireComponent(typeof(PlayerInput))]
    #endregion
    public class PlayerMovement : MonoBehaviour {
        private new Rigidbody rigidbody;
        private PlayerAnimator playerAnimator;
        private PlayerInput playerInput;

        private Transform mainCameraTransform;
        private Vector3 moveDirection;

        [Header("Layers")]
        [SerializeField] LayerMask groundLayer;

        [Header("Flags")]
        [SerializeField] bool isGrounded;

        [Header("Movement")]
        [SerializeField] float walkingSpeed = 3;
        [SerializeField] float runningSpeed = 7;
        [SerializeField] float sprintingSpeed = 10;
        [SerializeField] float rotateSpeed = 15;



        private void Awake() {
            rigidbody = GetComponent<Rigidbody>();
            playerAnimator = GetComponent<PlayerAnimator>();
            playerInput = GetComponent<PlayerInput>();

            mainCameraTransform = Camera.main.transform;
        }



        public void HandlePlayerMovements() {
            HandleSlope();
            HandleMove();
            HandleRotate();
        }



        public void HandleSlope() {
            RaycastHit hitInfo;

            Vector3 raycastOrigin = transform.position;
            raycastOrigin.y += 1f;

            Vector3 targetPoint = transform.position;

            if (Physics.SphereCast(raycastOrigin, 0.2f, Vector3.down, out hitInfo, 1f, groundLayer)) {
                Vector3 raycastHitPoint = hitInfo.point;
                targetPoint.y = raycastHitPoint.y;
            }

            if (isGrounded) {
                if (playerInput.MoveAmount() > 0)
                    transform.position = Vector3.Lerp(transform.position, targetPoint, Time.deltaTime / 0.1f);
                else
                    transform.position = targetPoint;
            }
        }



        private void HandleMove() {
            moveDirection = mainCameraTransform.forward * playerInput.verticalInput;
            moveDirection = moveDirection + mainCameraTransform.right * playerInput.horizontalInput;
            moveDirection.Normalize();
            moveDirection.y = 0;

            if (playerInput.IsSprinting())
                moveDirection = moveDirection * sprintingSpeed;
            else
            if (playerInput.MoveAmount() >= 0.5f)
                moveDirection = moveDirection * runningSpeed;
            else
                moveDirection = moveDirection * walkingSpeed;

            Vector3 movementVelocity = moveDirection;
            rigidbody.velocity = movementVelocity;
        }



        private void HandleRotate() {
            Vector3 targetDirection = Vector3.zero;

            targetDirection = mainCameraTransform.forward * playerInput.verticalInput;
            targetDirection = targetDirection + mainCameraTransform.right * playerInput.horizontalInput;
            targetDirection.Normalize();
            targetDirection.y = 0;

            if (targetDirection == Vector3.zero) {
                targetDirection = transform.forward;
            }

            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);

            transform.rotation = playerRotation;
        }
    }
}
