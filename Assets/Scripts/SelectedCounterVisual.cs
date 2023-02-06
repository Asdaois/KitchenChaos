using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour {
  [SerializeField] private BaseCounter baseCounter;
  [SerializeField] private GameObject[] visualSelectedCounter;

  private void Start() {
    Player.Instance.OnSelectedCounterChange += Player_OnSelectedCounterChange;
  }

  private void Player_OnSelectedCounterChange(object sender, Player.OnSelectedCounterChangeEventArgs e) {
    if (baseCounter == e.selectedCounter) {
      Show();
    } else {
      Hide();
    }
  }

  private void Hide() {
    foreach (var gameObject in visualSelectedCounter) {
      gameObject.SetActive(false);
    }
  }

  private void Show() {
    foreach (var gameObject in visualSelectedCounter) {
      gameObject.SetActive(true);
    }
  }
}