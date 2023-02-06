using UnityEngine;

public class KitchenObject : MonoBehaviour {

  [field: SerializeField]
  public KitchenObjectSO KitchenObjectSO { get; private set; }

  private IKitchenObjectParent kitchenObjectParent;

  public IKitchenObjectParent GetClearCounter() {
    return kitchenObjectParent;
  }

  public void SetKitchenObjepctParent(IKitchenObjectParent aKitchenObjectParent) {
    // Clean the previous counter
    kitchenObjectParent?.ClearKitchenObject();

    kitchenObjectParent = aKitchenObjectParent;

    if (kitchenObjectParent.HasKitchenObject()) {
      Debug.LogError("Kitchen Object has already have a kitchen object");
    }

    kitchenObjectParent.SetKitchenObject(this);
    ColocateAtCounterTop();
  }

  private void ColocateAtCounterTop() {
    transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();
    transform.localPosition = Vector3.zero;
  }
}