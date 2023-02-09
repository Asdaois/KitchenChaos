using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter {
  private enum State {
    Idle,
    Frying,
    Fried,
    Burned
  }

  [SerializeField] private List<FryingRecipeSO> recipes;

  private float fryingTimer;
  private FryingRecipeSO currentRecipe;
  private State currentState;

  private State CurrentState {
    get => currentState; set {
      currentState = value;
      Debug.Log(currentState);
    }
  }

  private void Start() {
    CurrentState = State.Idle;
  }

  private void Update() {
    HandleState();
  }

  private void HandleState() {
    if (!HasKitchenObject()) {
      return;
    }

    switch (CurrentState) {
      case State.Idle:
        break;
      case State.Frying:
        HandleFrying();
        break;
      case State.Fried:
        break;
      case State.Burned:
        break;
    }
  }

  private void HandleFrying() {
    fryingTimer += Time.deltaTime;

    if (fryingTimer < GetFryingTimeMaximum()) {
      return;
    }

    CurrentState = State.Fried;
    CookKitchenObject();
  }

  private void CookKitchenObject() {
    GetKitchenObject().DestroySelf();
    KitchenObject.Spawn(currentRecipe.Output, this);
  }

  private float GetFryingTimeMaximum() {
    return GetFryingRecipeSOFromInput(GetKitchenObject().KitchenObjectSO).FryingTimerMax;
  }

  public override void Interact(Player aPlayer) {
    if (CanInteractWithWitchenObjectFromPlayer(aPlayer)) {
      aPlayer.GetKitchenObject().SetKitchenObjepctParent(this);
      currentRecipe = GetFryingRecipeSOFromInput(GetKitchenObject().KitchenObjectSO);
      CurrentState = State.Frying;
      fryingTimer = 0f;
      return;
    }

    if (CanPlayerRetrieveKitchenObject(aPlayer)) {
      GetKitchenObject().SetKitchenObjepctParent(aPlayer);
    }
  }

  private bool CanPlayerRetrieveKitchenObject(Player aPlayer) {
    return HasKitchenObject() && !aPlayer.HasKitchenObject();
  }

  private bool CanInteractWithWitchenObjectFromPlayer(Player aPlayer) {
    return !HasKitchenObject()
             && aPlayer.HasKitchenObject()
             && HasRecipeWithKitchenObject(aPlayer.GetKitchenObject().KitchenObjectSO);
  }

  public override void InteractAlternative(Player aPlayer) {
    if (!HasKitchenObject()
        || !HasRecipeWithKitchenObject(GetKitchenObject().KitchenObjectSO)) {
      return;
    }
  }

  private KitchenObjectSO GetOutputFromInput(KitchenObjectSO aKitchenObjectSO) {
    var fryingRecipeSO = GetFryingRecipeSOFromInput(aKitchenObjectSO);

    return fryingRecipeSO != null ? fryingRecipeSO.Output : null;
  }

  private bool HasRecipeWithKitchenObject(KitchenObjectSO aKitchenObjectSO) {
    return GetFryingRecipeSOFromInput(aKitchenObjectSO) != null;
  }

  private FryingRecipeSO GetFryingRecipeSOFromInput(KitchenObjectSO aKitchenObjectSO) {
    foreach (var recipe in recipes) {
      if (recipe.Input == aKitchenObjectSO) {
        return recipe;
      }
    }

    return null;
  }
}
