using UnityEngine;

public class ClearCounter : MonoBehaviour {
  [field: SerializeField] public KitchenObject KitchenObject { get; set; }

  [SerializeField] private KitchenObjectSO kitchenObjectSO;
  [field: SerializeField] public Transform CounterTopPoint { get; private set; }

  [SerializeField] private Transform otherCounter;

  [SerializeField] private bool test;


  public bool HasKitchenObject => KitchenObject != null;

  private void Update() {
    if (test && Input.GetKey(KeyCode.Y)) {
      if (KitchenObject != null) {
        KitchenObject.ClearCounter = otherCounter.GetComponent<ClearCounter>();
      }
    }
  }

  public void Interact() {
    if (KitchenObject != null) {
      Debug.Log(KitchenObject.ClearCounter);
      return;
    }

    var kitchenObjectTransform = Instantiate(kitchenObjectSO.Prefab, CounterTopPoint);
    kitchenObjectTransform.GetComponent<KitchenObject>().ClearCounter = this;
  }

  public void ClearKitchenObject() {
    KitchenObject = null;
  }
}
