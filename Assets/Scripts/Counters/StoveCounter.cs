using System;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter, IHasProgress {

  public enum StoveCounterState {
    Idle,
    Frying,
    Fried,
    Burned
  }

  public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
  public event EventHandler<IHasProgress.OnProgressChangeEventArgs> OnProgressChange;

  public class OnStateChangedEventArgs : EventArgs {
    public StoveCounterState state;
  }

  [SerializeField] private List<FryingRecipeSO> fryings;
  [SerializeField] private List<BurnigRecipeSO> burnigs;

  private float fryingTimer;
  private float burningTimer;
  private FryingRecipeSO friedRecipe;
  private BurnigRecipeSO burnedRecipe;
  private StoveCounterState currentState;

  private StoveCounterState CurrentState {
    get => currentState; set {
      currentState = value;
      OnStateChanged?.Invoke(this, new() {
        state = value
      })
      ;
    }
  }

  public float FryingTimer {
    get => fryingTimer; set {
      fryingTimer = value;
      OnProgressChange?.Invoke(this, new() {
        progressNormalized = fryingTimer / friedRecipe.FryingTimerMax
      });
    }
  }
  public float BurningTimer {
    get => burningTimer; set {
      burningTimer = value;
      OnProgressChange?.Invoke(this, new() {
        progressNormalized = burningTimer / burnedRecipe.FryingTimerMax
      });
    }
  }

  private void Start() {
    CurrentState = StoveCounterState.Idle;
  }

  private void Update() {
    HandleState();
  }

  private void HandleState() {
    if (!HasKitchenObject()) {
      return;
    }

    switch (CurrentState) {
      case StoveCounterState.Idle:
        break;
      case StoveCounterState.Frying:
        HandleFrying();
        break;
      case StoveCounterState.Fried:
        HandleFried();
        break;
      case StoveCounterState.Burned:
        break;
    }
  }

  private void HandleFried() {
    BurningTimer += Time.deltaTime;

    if (BurningTimer < burnedRecipe.FryingTimerMax) {
      return;
    }

    CurrentState = StoveCounterState.Burned;
    GetKitchenObject().DestroySelf();
    KitchenObject.Spawn(burnedRecipe.Output, this);
  }

  private void HandleFrying() {
    FryingTimer += Time.deltaTime;

    if (FryingTimer < GetFryingTimeMaximum(friedRecipe)) {
      return;
    }

    CurrentState = StoveCounterState.Fried;
    BurningTimer = 0f;
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
      burnedRecipe = GetBurnedRecipeSOFromInput(friedRecipe.Output);
      FryingTimer = 0f;
      CurrentState = StoveCounterState.Frying;
      return;
    }

    if (CanPlayerRetrieveKitchenObject(aPlayer)) {
      GetKitchenObject().SetKitchenObjepctParent(aPlayer);
      CurrentState = StoveCounterState.Idle;
      OnProgressChange?.Invoke(this, new() { progressNormalized = 0 });
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
    foreach (var recipe in fryings) {
      if (recipe.Input == aKitchenObjectSO) {
        return recipe;
      }
    }

    return null;
  }

  private BurnigRecipeSO GetBurnedRecipeSOFromInput(KitchenObjectSO aKitchenObjectSO) {
    foreach (var recipe in burnigs) {
      if (recipe.Input == aKitchenObjectSO) {
        return recipe;
      }
    }

    return null;
  }
}
