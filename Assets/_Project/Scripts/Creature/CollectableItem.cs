using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Playtipes.InteractionSystem;

namespace Playtipes.Creature {
    public class CollectableItem : MonoBehaviour, IInteractable {
        public bool Interact(PlayerInteractor _interactor) {
            Destroy(gameObject);
            return true;
        }
    }
}
