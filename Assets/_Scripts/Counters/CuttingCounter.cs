﻿using System;
using UnityEngine;

public class CuttingCounter : BaseCounter, IHasProgress {
  public static event EventHandler<OnAnyCutEventArgs> OnAnyCut;
  public class OnAnyCutEventArgs : EventArgs {
    public Vector3 position;
  }

  public event EventHandler OnCut;
  public event EventHandler<IHasProgress.OnProgressChangeEventArgs> OnProgressChange;


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

    if (IsHoldingAPlate(aPlayer)) {
      if (aPlayer.GetKitchenObject().TryGetPlate(out var plateKitchenObject)
        && !plateKitchenObject.TryAddIngredient(GetKitchenObject().KitchenObjectSO)) {
        return;
      }
      GetKitchenObject().DestroySelf();
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
    OnAnyCut?.Invoke(this, new() { position = transform.position });
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