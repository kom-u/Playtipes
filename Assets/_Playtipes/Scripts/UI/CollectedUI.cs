using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Playtipes.Core;
using Playtipes.InteractionSystem;

namespace Playtipes.UI {
    public class CollectedUI : MonoBehaviour {
        [SerializeField] private TextMeshProUGUI collectedTextMP;



        private void OnEnable() {
            InteractEvent.Instance.OnCollect += InteractEvent_OnCollect;
        }



        private void OnDisable() {
            InteractEvent.Instance.OnCollect -= InteractEvent_OnCollect;
        }



        private void InteractEvent_OnCollect(InteractEvent _sender) {
            ChangeCollectedText();
        }



        private void ChangeCollectedText() {
            collectedTextMP.text = GameManager.Instance.GetCollectedText();
        }
    }
}
