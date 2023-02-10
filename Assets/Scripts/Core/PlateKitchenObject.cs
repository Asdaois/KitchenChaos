using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject {
  [SerializeField] private List<KitchenObjectSO> validIngredients;

  private List<KitchenObjectSO> ingredients = new();
  public bool TryAddIngredient(KitchenObjectSO aIngredient) {
    if (!validIngredients.Contains(aIngredient))
      return false;

    if (ingredients.Contains(aIngredient))
      return false;

    ingredients.Add(aIngredient);
    return true;
  }

}
