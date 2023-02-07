using UnityEngine;

public class ClearCounter : BaseCounter {
  [SerializeField] private KitchenObjectSO kitchenObjectSO;

  public override void Interact(Player aPlayer) {
    if (!HasKitchenObject() && aPlayer.HasKitchenObject()) {
      aPlayer.GetKitchenObject().SetKitchenObjepctParent(this);
      return;
    }

    if (HasKitchenObject() && !aPlayer.HasKitchenObject()) {
      GetKitchenObject().SetKitchenObjepctParent(aPlayer);
    }
  }
}