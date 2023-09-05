using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Playtipes.InputSystem;

namespace Playtipes.Player {
    [DisallowMultipleComponent]
    #region RequireComponents
    [RequireComponent(typeof(PlayerAnimator))]
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(PlayerMovement))]
    #endregion
    public class PlayerManager : MonoBehaviour {
        private PlayerAnimator playerAnimator;
        private PlayerInput playerInput;
        private PlayerMovement playerMovement;

        private bool isHit = false;

        private void Awake() {
            playerAnimator = GetComponent<PlayerAnimator>();
            playerInput = GetComponent<PlayerInput>();
            playerMovement = GetComponent<PlayerMovement>();

        }



        private void OnCollisionEnter(Collision other) {
            if (!isHit
            && other.gameObject.CompareTag("Obstacle"))
                playerAnimator.PlayHitAnimation();
        }




        private void Update() {
            if (playerAnimator.IsOverrideAnimationPlay()) {
                playerAnimator.ResetMoveParamValue();
                playerInput.ResetInputValue();
                isHit = true;
                return;
            }
            isHit = false;

            playerInput.HandlePlayerInputs();

            playerAnimator.UpdateAnimatorParameter(0, playerInput.MoveAmount(), playerInput.IsSprinting());
        }



        private void FixedUpdate() {
            playerMovement.HandlePlayerMovements();
        }




    }
}
