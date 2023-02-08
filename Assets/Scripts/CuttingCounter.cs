using UnityEngine;

public class CuttingCounter : BaseCounter {
  [SerializeField] private CuttingRecipeSO[] cuttinRecipes;

  public override void Interact(Player aPlayer) {
    if (!HasKitchenObject()
      && aPlayer.HasKitchenObject()
      && HasRecipeWithKitchenObject(aPlayer.GetKitchenObject().KitchenObjectSO)) {
      aPlayer.GetKitchenObject().SetKitchenObjepctParent(this);
      return;
    }

    if (HasKitchenObject() && !aPlayer.HasKitchenObject()) {
      GetKitchenObject().SetKitchenObjepctParent(aPlayer);
    }
  }

  public override void InteractAlternative(Player aPlayer) {
    if (!HasKitchenObject()
        || !HasRecipeWithKitchenObject(GetKitchenObject().KitchenObjectSO)) {
      return;
    }

    var outputKitchenObjectSO = GetOutputFromInput(GetKitchenObject().KitchenObjectSO);

    GetKitchenObject().DestroySelf();
    KitchenObject.Spawn(outputKitchenObjectSO, this);
  }

  private KitchenObjectSO GetOutputFromInput(KitchenObjectSO aKitchenObjectSO) {
    foreach (var cuttingRecipe in cuttinRecipes) {
      if (cuttingRecipe.Input == aKitchenObjectSO) {
        return cuttingRecipe.Output;
      }
    }

    return null;
  }

  private bool HasRecipeWithKitchenObject(KitchenObjectSO aKitchenObjectSO) {
    foreach (var cuttingRecipe in cuttinRecipes) {
      if (cuttingRecipe.Input == aKitchenObjectSO) {
        return true;
      }
    }

    return false;
  }
}