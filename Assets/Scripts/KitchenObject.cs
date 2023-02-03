using UnityEngine;

public class KitchenObject : MonoBehaviour {

  [field: SerializeField]
  public KitchenObjectSO KitchenObjectSO { get; private set; }

  private IKitchenObjectParent clearCounter;

  public IKitchenObjectParent GetClearCounter() {
    return clearCounter;
  }

  public void SetKitchenObjepctParent(IKitchenObjectParent value) {
    if (clearCounter != null) {
      // Clean the previous counter
      clearCounter.ClearKitchenObject();
    }

    clearCounter = value;

    if (clearCounter.GetHasKitchenObject()) {
      Debug.LogError("Counter has already have a kitchen object");
    }

    clearCounter.SetKitchenObject(this);
    ColocateAtCounterTop();
  }

  private void ColocateAtCounterTop() {
    transform.parent = clearCounter.GetKitchenObjectFollowTransform();
    transform.localPosition = Vector3.zero;
  }
}
