using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryManagerRecipeUI : MonoBehaviour {
  [SerializeField] private TextMeshProUGUI text;
  [SerializeField] private Transform iconsContainer;
  [SerializeField] private Transform iconTemplate;

  private void Start() {
    iconTemplate.gameObject.SetActive(false);
  }

  public void SetRecipe(RecipeSO aRecipeSO) {
    text.text = aRecipeSO.GetRecipeName();

    foreach (Transform child in iconsContainer) {
      if (child == iconTemplate)
        continue;
      Destroy(child.gameObject);
    }

    foreach (var ingredient in aRecipeSO.GetIngredients()) {
      var iconTransform = Instantiate(iconTemplate, iconsContainer);
      iconTransform.gameObject.SetActive(true);
      iconTransform.GetComponent<Image>().sprite = ingredient.Sprite;
    }
  }

}
