using System.Runtime.CompilerServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Playtipes.InteractionSystem;

namespace Playtipes {
    public class PlayerAnimator : MonoBehaviour {
        [SerializeField] private Animator animator;

        private int horizontal = Animator.StringToHash("Horizontal");
        private int vertical = Animator.StringToHash("Vertical");
        private float dampTime = 0.1f;



        private void OnEnable() {
            InteractEvent.Instance.OnCollect += InteractEvent_OnCollect;
        }



        private void OnDisable() {
            InteractEvent.Instance.OnCollect -= InteractEvent_OnCollect;
        }



        private void InteractEvent_OnCollect(InteractEvent _sender) {
            PlayPickupAnimation();
        }



        public void PlayHitAnimation() {
            StartCoroutine(HitAnimationCoroutine());
        }



        private IEnumerator HitAnimationCoroutine() {
            animator.SetBool("IsHit", true);
            animator.Play("Chara_FallingDown", 0);

            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

            animator.Play("Chara_GettingUp", 0);

            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length * 0.5f);
            animator.SetBool("IsHit", false);
        }



        public void PlayPickupAnimation() {
            StartCoroutine(PickUpAnimationCoroutine());
        }



        private IEnumerator PickUpAnimationCoroutine() {
            animator.SetBool("IsPickUp", true);
            animator.Play("Chara_PickUp", 0);

            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length * 0.5f);

            animator.SetBool("IsPickUp", false);
        }



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



        public bool IsOverrideAnimationPlay() {
            return IsHitAnimationPlay() || IsPickUpAnimationPlay();
        }



        private bool IsHitAnimationPlay() {
            return animator.GetBool("IsHit");
        }



        private bool IsPickUpAnimationPlay() {
            return animator.GetBool("IsPickUp");
        }



        public void ResetMoveParamValue() {
            animator.SetFloat(horizontal, 0);
            animator.SetFloat(vertical, 0);
        }
    }
}
