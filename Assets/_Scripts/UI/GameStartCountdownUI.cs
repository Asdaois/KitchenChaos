using TMPro;
using UnityEngine;

public class GameStartCountdownUI : MonoBehaviour {
  [SerializeField] private TextMeshProUGUI textMesh;

  private void Start() {
    KitchenGameManager.Instance.OnStateChanged += KitchenGameManager_OnStateChanged;
  }

  private void Update() {
    textMesh.text = Mathf.Ceil(KitchenGameManager.Instance.GetCurrentCountDownTimerTime())
                         .ToString();
  }

  private void KitchenGameManager_OnStateChanged(object sender, System.EventArgs e) {
    if (!KitchenGameManager.Instance.IsCountdownToSTartActive()) {
      Hide();
      return;
    }

    Show();
  }

  private void Hide() {
    gameObject.SetActive(false);
  }

  private void Show() {
    gameObject.SetActive(true);
  }
}
