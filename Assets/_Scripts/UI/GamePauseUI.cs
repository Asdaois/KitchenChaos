using UnityEngine;

public class GamePauseUI : MonoBehaviour {

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
