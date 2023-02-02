using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour {
  [SerializeField] private ClearCounter clearCounter;
  [SerializeField] private GameObject visualSelectedCounter;
  private void Start() {
    Player.Instance.OnSelectedCounterChange += Player_OnSelectedCounterChange;
  }

  private void Player_OnSelectedCounterChange(object sender, Player.OnSelectedCounterChangeEventArgs e) {
    if (clearCounter == e.selectedCounter) {
      Show();
    } else {
      Hide();
    }
  }

  private void Hide() {
    visualSelectedCounter.SetActive(false);
  }

  private void Show() {
    visualSelectedCounter.SetActive(true);
  }
}
