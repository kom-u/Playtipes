using UnityEngine;
using Playtipes.InteractionSystem;

namespace Playtipes.Core {
    [DisallowMultipleComponent]
    #region RequireComponents
    [RequireComponent(typeof(GameEvent))]
    #endregion
    public class GameManager : SingletonMonobehaviour<GameManager> {
        [HideInInspector] public GameEvent gameEvent;

        [Header("Objective")]
        [SerializeField] private int collectObjective;

        private int collectedCount;
        private float playTimer;



        private new void Awake() {
            base.Awake();

            gameEvent = GetComponent<GameEvent>();
        }



        private void Update() {
            playTimer += Time.deltaTime;
        }



        private void OnEnable() {
            gameEvent.OnStart += GameEvent_OnStart;
            InteractEvent.Instance.OnCollect += InteractEvent_OnCollect;
        }



        private void OnDisable() {
            gameEvent.OnStart -= GameEvent_OnStart;
            InteractEvent.Instance.OnCollect -= InteractEvent_OnCollect;
        }



        private void GameEvent_OnStart(GameEvent _gameEvent) {
            collectedCount = 0;
            playTimer = 0;
        }



        private void InteractEvent_OnCollect(InteractEvent _interactEvent) {
            AddCollectedCount();
            if (IsObjectiveCompleted()) {
                gameEvent.CallOnObjectiveCompletedEvent();
            }
        }



        public string GetCollectedText() {
            return $"{collectedCount}/{collectObjective}\nCollected";
        }



        public string GetPlayTimerText() {
            return $"Timer\n{((int)playTimer / 60).ToString("00")}:{((int)playTimer % 60).ToString("00")}:{((int)(playTimer * 100) % 100).ToString("00")}";
        }



        private void AddCollectedCount() {
            collectedCount++;
        }



        private bool IsObjectiveCompleted() {
            return CheckCollectObjective();
        }



        private bool CheckCollectObjective() {
            if (collectedCount >= collectObjective) {
                gameEvent.CallOnObjectiveCompletedEvent();
                return true;
            }

            return false;
        }
    }
}
