using UnityEngine;

public class PlateIconsUI : MonoBehaviour {
  [SerializeField] private PlateKitchenObject plateKitchenObject;
  [SerializeField] private Transform iconTemplate;

  private void Awake() {
    iconTemplate.gameObject.SetActive(false);
  }

  private void Start() {
    plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
  }

  private void PlateKitchenObject_OnIngredientAdded(object sender, OnIngredientAddedEventArgs e) {
    UpdateVisual();
  }

  private void UpdateVisual() {
    foreach (Transform child in transform) {
      if (child != iconTemplate) {
        Destroy(child.gameObject);
      }
    }

    plateKitchenObject.GetIngredients().ForEach(ingredient => {

      var icon = Instantiate(iconTemplate, transform);
      icon.gameObject.SetActive(true);
      icon.GetComponent<PlateIconUI>().SetKitchenObjectSO(ingredient);
    });
  }
}
