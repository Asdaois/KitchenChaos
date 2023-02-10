using UnityEngine;

public class ClearCounter : BaseCounter {
  [SerializeField] private KitchenObjectSO kitchenObjectSO;

  public override void Interact(Player aPlayer) {
    if (IsGivingAKitchenObject(aPlayer)) {
      aPlayer.GetKitchenObject().SetKitchenObjepctParent(this);
      return;
    }


    if (IsRetrivingKitchenObject(aPlayer)) {
      GetKitchenObject().SetKitchenObjepctParent(aPlayer);
    }


    if (IsHoldingAPlate(aPlayer)) {
      if (aPlayer.GetKitchenObject().TryGetPlate(out var plateKitchenObject)) {
        if (!plateKitchenObject.TryAddIngredient(GetKitchenObject().KitchenObjectSO)) {
          return;
        }
      }
      GetKitchenObject().DestroySelf();
    }


    if (IsHoldingAPlate(this) && aPlayer.HasKitchenObject()) {
      if (GetKitchenObject().TryGetPlate(out var plateKitchenObject)) {
        var isIngredientAdded = plateKitchenObject.TryAddIngredient(aPlayer.GetKitchenObject().KitchenObjectSO);

        if (isIngredientAdded) {
          aPlayer.GetKitchenObject().DestroySelf();
        }
      }
    }
  }

  private bool IsGivingAKitchenObject(Player aPlayer) {
    return !HasKitchenObject() && aPlayer.HasKitchenObject();
  }

  private bool IsRetrivingKitchenObject(Player aPlayer) {
    return HasKitchenObject() && !aPlayer.HasKitchenObject();
  }


}