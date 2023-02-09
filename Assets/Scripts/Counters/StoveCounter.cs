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
  private float burningTimer;
  private FryingRecipeSO friedRecipe;
  private FryingRecipeSO burnedRecipe;
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
        HandleFried();
        break;
      case State.Burned:
        break;
    }
  }

  private void HandleFried() {
    burningTimer += Time.deltaTime;

    if (burningTimer < GetFryingTimeMaximum(burnedRecipe)) {
      return;
    }

    CurrentState = State.Burned;
    SpawnKitchenObject(burnedRecipe);
  }

  private void HandleFrying() {
    fryingTimer += Time.deltaTime;

    if (fryingTimer < GetFryingTimeMaximum(friedRecipe)) {
      return;
    }

    CurrentState = State.Fried;
    burningTimer = 0f;
    SpawnKitchenObject(friedRecipe);
  }

  private void SpawnKitchenObject(FryingRecipeSO recipe) {
    GetKitchenObject().DestroySelf();
    KitchenObject.Spawn(recipe.Output, this);
  }

  private float GetFryingTimeMaximum(FryingRecipeSO fryingRecipeSO) {
    return fryingRecipeSO.FryingTimerMax;
  }

  public override void Interact(Player aPlayer) {
    if (CanInteractWithWitchenObjectFromPlayer(aPlayer)) {
      aPlayer.GetKitchenObject().SetKitchenObjepctParent(this);
      friedRecipe = GetFryingRecipeSOFromInput(GetKitchenObject().KitchenObjectSO);
      burnedRecipe = GetFryingRecipeSOFromInput(friedRecipe.Output);
      fryingTimer = 0f;
      CurrentState = State.Frying;
      return;
    }

    if (CanPlayerRetrieveKitchenObject(aPlayer)) {
      GetKitchenObject().SetKitchenObjepctParent(aPlayer);
      currentState = State.Idle;
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
