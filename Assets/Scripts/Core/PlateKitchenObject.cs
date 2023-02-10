using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject {
  public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
  [SerializeField] private List<KitchenObjectSO> validIngredients;

  private List<KitchenObjectSO> ingredients = new();
  public bool TryAddIngredient(KitchenObjectSO aIngredient) {
    if (!validIngredients.Contains(aIngredient))
      return false;

    if (ingredients.Contains(aIngredient))
      return false;

    ingredients.Add(aIngredient);
    OnIngredientAdded?.Invoke(this, new() { ingredient = aIngredient });
    return true;
  }
}

public class OnIngredientAddedEventArgs {
  public KitchenObjectSO ingredient;
}
