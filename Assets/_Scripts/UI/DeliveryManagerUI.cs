using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour {

  [SerializeField] private Transform container;
  [SerializeField] private Transform recipeTemplate;

  private void Awake() {
    recipeTemplate.gameObject.SetActive(false);
  }

  private void Start() {
    DeliveryManager.Instance.OnWaitingRecipeChange += DeliveryManager_OnWaitingRecipeChange;
    Display();
  }

  private void DeliveryManager_OnWaitingRecipeChange(object sender, System.EventArgs e) {
    Display();
  }

  private void Display() {
    foreach (Transform child in container) {
      if (child == recipeTemplate) {
        continue;
      }
      Destroy(child.gameObject);
    }

    foreach (var recipe in DeliveryManager.Instance.GetWaitingRecipes()) {
      var recipeTransform = Instantiate(recipeTemplate, container);
      recipeTransform.gameObject.SetActive(true);
      recipeTransform.GetComponent<DeliveryManagerRecipeUI>().SetRecipe(recipe);
    }
  }
}
