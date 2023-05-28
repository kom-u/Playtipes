using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Playtipes.InteractionSystem;

namespace Playtipes.UI {
    public class PromptUI : MonoBehaviour {
        [SerializeField] private GameObject promptGroup;
        [SerializeField] private TextMeshProUGUI promptTextMP;



        private void OnEnable() {
            InteractEvent.Instance.OnRange += InteractEvent_OnRange;
        }



        private void OnDisable() {
            InteractEvent.Instance.OnRange -= InteractEvent_OnRange;
        }



        private void InteractEvent_OnRange(InteractEvent _sender, bool _state) {
            promptGroup.SetActive(_state);
        }



        private void ChangePromptText(string _text) {
            promptTextMP.text = _text;
        }
    }
}