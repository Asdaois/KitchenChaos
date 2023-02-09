using UnityEngine;

public class CuttingCounter : BaseCounter {
  [SerializeField] private CuttingRecipeSO[] cuttinRecipes;
  private int cuttingProgress;
  public override void Interact(Player aPlayer) {
    if (!HasKitchenObject()
      && aPlayer.HasKitchenObject()
      && HasRecipeWithKitchenObject(aPlayer.GetKitchenObject().KitchenObjectSO)) {
      aPlayer.GetKitchenObject().SetKitchenObjepctParent(this);
      cuttingProgress = 0;
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

    cuttingProgress++;
    var cuttingRecipeSO = GetCuttingRecipeSOFromInput(GetKitchenObject().KitchenObjectSO);

    if (cuttingProgress < cuttingRecipeSO.CuttingProgresMaximum ) {
      return;
    }

    var outputKitchenObjectSO = GetOutputFromInput(GetKitchenObject().KitchenObjectSO);

    GetKitchenObject().DestroySelf();
    KitchenObject.Spawn(outputKitchenObjectSO, this);
  }

  private KitchenObjectSO GetOutputFromInput(KitchenObjectSO aKitchenObjectSO) {
    var cuttingRecipeSO = GetCuttingRecipeSOFromInput(aKitchenObjectSO);

    return cuttingRecipeSO != null ? cuttingRecipeSO.Output : null;
  }

  private bool HasRecipeWithKitchenObject(KitchenObjectSO aKitchenObjectSO) {
    return GetCuttingRecipeSOFromInput(aKitchenObjectSO) != null;
  }

  private CuttingRecipeSO GetCuttingRecipeSOFromInput(KitchenObjectSO aKitchenObjectSO) {
    foreach (var cuttingRecipe in cuttinRecipes) {
      if (cuttingRecipe.Input == aKitchenObjectSO) {
        return cuttingRecipe;
      }
    }

    return null;
  }
}