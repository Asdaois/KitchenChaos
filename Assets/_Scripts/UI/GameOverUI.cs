using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour {

  [SerializeField] private TextMeshProUGUI recipesDeliveredTextUI;

  private void Start() {
    KitchenGameManager.Instance.OnStateChanged += KitchenGameManager_OnStateChanged;
    Hide();
  }

  private void Update() {
  }

  private void UpdateUI() {
    recipesDeliveredTextUI.text = DeliveryManager.Instance.GetRecipesDeliveredAmount()
                                                              .ToString();
  }

  private void KitchenGameManager_OnStateChanged(object sender, System.EventArgs e) {
    if (!KitchenGameManager.Instance.IsGameOverActive()) {
      Hide();
      return;
    }

    UpdateUI();
    Show();
  }

  private void Hide() {
    gameObject.SetActive(false);
  }

  private void Show() {
    gameObject.SetActive(true);
  }
}
