using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Playtipes.InputSystem;

namespace Playtipes {
    public class PlayerAnimator : MonoBehaviour {
        [SerializeField] private Animator animator;

        private int horizontal = Animator.StringToHash("Horizontal");
        private int vertical = Animator.StringToHash("Vertical");
        private float dampTime = 0.1f;



        public void UpdateAnimatorParameter(float _horizontalMovement, float _verticalMovement, bool _isSprinting) {
            float snappedHorizontal = SnapMovementValue(_horizontalMovement);
            float snappedVertical = SnapMovementValue(_verticalMovement);

            if (_isSprinting)
                snappedHorizontal = 2;

            animator.SetFloat(horizontal, snappedHorizontal, dampTime, Time.deltaTime);
            animator.SetFloat(vertical, snappedVertical, dampTime, Time.deltaTime);
        }



        private float SnapMovementValue(float _value) {
            if (_value > 0
            && _value < 0.55f)
                return 0.5f;

            else
            if (_value > 0.55f)
                return 1;

            else
            if (_value < 0
            && _value > -0.55f)
                return -0.5f;

            else
            if (_value < -0.55f)
                return -1;

            else
                return 0;
        }
    }
}
