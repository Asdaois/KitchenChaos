using System.Collections.Generic;

public class PlateKitchenObject : KitchenObject {

  private List<KitchenObjectSO> ingredients = new();
  public void AddIngredient(KitchenObjectSO aIngredient) {
    ingredients.Add(aIngredient);
  }

}
