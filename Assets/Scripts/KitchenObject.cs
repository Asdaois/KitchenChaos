using UnityEngine;

public class KitchenObject : MonoBehaviour {

  [field: SerializeField]
  public KitchenObjectSO KitchenObjectSO { get; private set; }

  private ClearCounter clearCounter;
  public ClearCounter ClearCounter {
    get => clearCounter;
    set {
      if (clearCounter != null) {
        // Clean the previous counter
        clearCounter.ClearKitchenObject();
      }

      clearCounter = value;

      if (clearCounter.HasKitchenObject) {
        Debug.LogError("Counter has already have a kitchen object");
      }

      clearCounter.KitchenObject = this;
      ColocateAtCounterTop();
    }
  }

  private void ColocateAtCounterTop() {
    transform.parent = clearCounter.CounterTopPoint;
    transform.localPosition = Vector3.zero;
  }
}
