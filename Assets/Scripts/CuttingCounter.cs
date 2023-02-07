using UnityEngine;

public class CuttingCounter : BaseCounter {
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

  public override void InteractAlternative(Player aPlayer) {
    if (!HasKitchenObject()) {
      return;
    }

    Debug.Log(HasKitchenObject());
    GetKitchenObject().DestroySelf();
    KitchenObject.Spawn(kitchenObjectSO, this);
  }
}