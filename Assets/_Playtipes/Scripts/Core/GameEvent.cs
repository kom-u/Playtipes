using System;
using UnityEngine;

namespace Playtipes.Core {
    [DisallowMultipleComponent]
    public class GameEvent : MonoBehaviour {
        public event Action<GameEvent> OnStart;
        public void CallOnStartEvent() {
            OnStart?.Invoke(this);
        }




        public event Action<GameEvent> OnObjectiveCompleted;
        public void CallOnObjectiveCompletedEvent() {
            OnObjectiveCompleted?.Invoke(this);
        }
    }
}
