using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Playtipes.Core;

namespace Playtipes {
    public class TimerUI : MonoBehaviour {
        [SerializeField] private TextMeshProUGUI playTimerTextMP;



        private void Update() {
            ChangePlayTimerText();
        }



        private void ChangePlayTimerText() {
            playTimerTextMP.text = GameManager.Instance.GetPlayTimerText();
        }
    }
}
