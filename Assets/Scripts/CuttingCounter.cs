using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class CuttingCounter : BaseCounter {
  public event EventHandler OnCut;
  public event EventHandler<OnProgressChangeEventArgs> OnProgressChange;
  public class OnProgressChangeEventArgs : EventArgs {
    public float progressNormalized;
  }

  [SerializeField] private CuttingRecipeSO[] cuttinRecipes;
  private int cuttingProgress;

  public int CuttingProgress {
    get => cuttingProgress; set {

      cuttingProgress = value;
      OnProgressChange?.Invoke(this, new() {
        progressNormalized = (float)cuttingProgress / GetCuttingProgressMaximum()
      });
    }
  }

  public override void Interact(Player aPlayer) {
    if (!HasKitchenObject()
      && aPlayer.HasKitchenObject()
      && HasRecipeWithKitchenObject(aPlayer.GetKitchenObject().KitchenObjectSO)) {
      aPlayer.GetKitchenObject().SetKitchenObjepctParent(this);
      CuttingProgress = 0;
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

    CutKitchenObject();

    if (CuttingProgress < GetCuttingProgressMaximum()) {
      return;
    }

    KitchenObjectSO outputKitchenObjectSO = GetOutputKitchenObject();

    CuttingProgress = 0;
    GetKitchenObject().DestroySelf();
    KitchenObject.Spawn(outputKitchenObjectSO, this);
  }

  private KitchenObjectSO GetOutputKitchenObject() {
    return GetOutputFromInput(GetKitchenObject().KitchenObjectSO);
  }

  private void CutKitchenObject() {
    CuttingProgress++;
    OnCut?.Invoke(this, EventArgs.Empty);
  }

  private int GetCuttingProgressMaximum() {
    return GetCuttingRecipeSOFromInput(GetKitchenObject().KitchenObjectSO).CuttingProgresMaximum;
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