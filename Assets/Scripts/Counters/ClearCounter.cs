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
      if (aPlayer.GetKitchenObject().TryGetPlate(out var plateKitchenObject)
        && !plateKitchenObject.TryAddIngredient(GetKitchenObject().KitchenObjectSO)) {
        return;
      }
      GetKitchenObject().DestroySelf();
    }
  }
  private static bool IsHoldingAPlate(IKitchenObjectParent aKitchenObjectParent) {

    return aKitchenObjectParent.HasKitchenObject()
           && aKitchenObjectParent.GetKitchenObject() is PlateKitchenObject;
  }

  private bool IsGivingAKitchenObject(Player aPlayer) {
    return !HasKitchenObject() && aPlayer.HasKitchenObject();
  }

  private bool IsRetrivingKitchenObject(Player aPlayer) {
    return HasKitchenObject() && !aPlayer.HasKitchenObject();
  }


}