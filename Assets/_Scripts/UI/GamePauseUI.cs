using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour {
  [SerializeField] private Button resumeButton;
  [SerializeField] private Button mainMenuButton;

  private void Awake() {
    mainMenuButton.onClick.AddListener(() => {
      Loader.LoadScene(Loader.Scene.MainMenuScene);
    });
    resumeButton.onClick.AddListener(() => {
      KitchenGameManager.Instance.ResumeGame();
    });
  }
  private void Start() {
    KitchenGameManager.Instance.OnGamePauseChanged += GameManager_OnGamePauseChanged;
    Hide();
  }

  private void GameManager_OnGamePauseChanged(object sender, System.EventArgs e) {
    if (KitchenGameManager.Instance.GetIsGamePaused())
      Show();
    else
      Hide();


  }

  private void Show() {
    gameObject.SetActive(true);
  }

  private void Hide() {
    gameObject.SetActive(false);
  }
}
