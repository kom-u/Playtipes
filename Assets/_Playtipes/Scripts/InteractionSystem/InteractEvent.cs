using System;
using UnityEngine;

namespace Playtipes.InteractionSystem {
    [DisallowMultipleComponent]
    public class InteractEvent : SingletonMonobehaviour<InteractEvent> {
        public event Action<InteractEvent> OnCollect;
        public void CallOnCollectEvent() {
            OnCollect?.Invoke(this);
        }

        public event Action<InteractEvent, bool> OnRange;
        public void CallOnRangeEvent(bool _state) {
            OnRange?.Invoke(this, _state);
        }
    }
}
