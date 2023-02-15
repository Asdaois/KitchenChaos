using UnityEngine;
using UnityEngine.UI;

public class GameClockUI : MonoBehaviour {
  [SerializeField] Image imageTimer;
  private void Update() {
    var kitchenManager = KitchenGameManager.Instance;
    if (kitchenManager.IsGamePlaying()) {
      imageTimer.fillAmount = kitchenManager.GetCurrentPlayedTimeInPercentage();
    }
  }
}
