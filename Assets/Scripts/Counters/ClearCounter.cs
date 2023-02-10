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

    if (IsHoldingAPlate(aPlayer, out var plateKitchenObject)) {
      plateKitchenObject.AddIngredient(GetKitchenObject().KitchenObjectSO);
      GetKitchenObject().DestroySelf();
    }
  }

  private bool IsGivingAKitchenObject(Player aPlayer) {
    return !HasKitchenObject() && aPlayer.HasKitchenObject();
  }

  private bool IsRetrivingKitchenObject(Player aPlayer) {
    return HasKitchenObject() && !aPlayer.HasKitchenObject();
  }

  private static bool IsHoldingAPlate(IKitchenObjectParent aKitchenObjectParent, out PlateKitchenObject plateKitchenObject) {
    plateKitchenObject = aKitchenObjectParent.GetKitchenObject() as PlateKitchenObject;
    ;
    return aKitchenObjectParent.HasKitchenObject()
           && aKitchenObjectParent.GetKitchenObject() is PlateKitchenObject;
  }
}