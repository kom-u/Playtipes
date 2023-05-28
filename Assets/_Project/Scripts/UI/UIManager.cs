using UnityEngine;
using UnityEngine.SceneManagement;
using Playtipes.Core;

namespace Playtipes.UI {
    public class UIManager : MonoBehaviour {
        [SerializeField] private GameObject menu;
        [SerializeField] private GameObject hud;
        [SerializeField] private GameObject end;



        private void Start() {
            Time.timeScale = 0;
            ShowMenu();
        }



        private void OnEnable() {
            GameManager.Instance.gameEvent.OnObjectiveCompleted += GameManager_GameEvent_OnObjectiveCompleted;
        }



        private void OnDisable() {
            GameManager.Instance.gameEvent.OnObjectiveCompleted -= GameManager_GameEvent_OnObjectiveCompleted;
        }



        private void GameManager_GameEvent_OnObjectiveCompleted(GameEvent _gameEvent) {
            ShowEnd();
        }



        public void StartGame() {
            Time.timeScale = 1;
            GameManager.Instance.gameEvent.CallOnStartEvent();
            ShowHUD();
        }



        public void RestartGame() {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }



        public void ShowMenu() {
            menu.SetActive(true);
            hud.SetActive(false);
            end.SetActive(false);
        }



        public void ShowHUD() {
            menu.SetActive(false);
            hud.SetActive(true);
            end.SetActive(false);
        }



        public void ShowEnd() {
            menu.SetActive(false);
            hud.SetActive(false);
            end.SetActive(true);
        }



        public void HideAll() {
            menu.SetActive(false);
            hud.SetActive(false);
            end.SetActive(false);
        }
    }
}
