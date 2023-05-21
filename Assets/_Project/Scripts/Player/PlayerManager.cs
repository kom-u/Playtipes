using System;
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
        PlayerAnimator playerAnimator;
        PlayerInput playerInput;
        PlayerMovement playerMovement;



        private void Awake() {
            playerAnimator = GetComponent<PlayerAnimator>();
            playerInput = GetComponent<PlayerInput>();
            playerMovement = GetComponent<PlayerMovement>();
        }



        private void Update() {
            playerInput.HandlePlayerInputs();

            playerAnimator.UpdateAnimatorParameter(0, playerInput.MoveAmount(), playerInput.IsSprinting());
        }



        private void FixedUpdate() {
            playerMovement.HandlePlayerMovements();
        }




    }
}
