using UnityEngine;
using Playtipes.InputSystem;

namespace Playtipes.InteractionSystem {
    public class PlayerInteractor : MonoBehaviour {
        PlayerInput playerInput;

        [SerializeField] private Transform interactionPoint;
        [SerializeField] private float interactionPointRadius = 0.5f;
        [SerializeField] private LayerMask interactableMask;

        private readonly Collider[] colliders = new Collider[3];
        [SerializeField] private int objectFound;

        IInteractable interactable;



        private void Awake() {
            playerInput = GetComponent<PlayerInput>();
        }



        private void Update() {
            CheckInteractableObject();
        }



        private void CheckInteractableObject() {
            // kan gue nyari tiap frame ada ga Trigger yang overlap sama layer Interactable
            objectFound = Physics.OverlapSphereNonAlloc(interactionPoint.position, interactionPointRadius,
                        colliders, interactableMask);


            // yo kalo ada
            if (objectFound > 0) {
                // gue ambil IInteractablenya, supaya bisa pake fungsi yang ada di IInteractable, yaitu InteractionPrompt, sama Interact
                interactable = colliders[0].GetComponent<IInteractable>();

                if (interactable != null) // kalo ada komponent IInteractable di gameobject yang overlap
                {
                    // // gue pasttin dulu, promptnya udah muncul apa belom, kalo belom ya munculin
                    // // tapih, kalo Dumpsternya juga belum kebuka, karna kalo kebuka gabole dimunculin
                    // if (!_interactionPromptUI.IsDisplayed && isDumpsterOpen == false) {
                    //     // nah jadi ini gue munculin
                    //     // karna dumpster belum kebuka, sama promptnya juga belom ada
                    //     _interactionPromptUI.SetUp(interactable.InteractionPrompt);
                    // }

                    InteractEvent.Instance.CallOnRangeEvent(true);
                    if (playerInput.IsInteracting()) {
                        interactable.Interact(this);
                        InteractEvent.Instance.CallOnCollectEvent();
                    }
                }
            }
            // kalo gada
            else {
                // kalau masih ada IInteractable yg kesimpan, biar ga bug, kita null in dulu
                InteractEvent.Instance.CallOnRangeEvent(false);
                if (interactable != null) interactable = null;

                // trus kalo promptnya aktif, yo matiin, biar ga riweh
                // if (_interactionPromptUI.IsDisplayed) _interactionPromptUI.CloseDisplayed();
            }
        }
    }
}
