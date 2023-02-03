using UnityEngine;

public class ClearCounter : MonoBehaviour, IKitchenObjectParent {
  [SerializeField] private KitchenObjectSO kitchenObjectSO;
  [SerializeField] private Transform counterTopPoint;
  [SerializeField] private Transform otherCounter;
  [SerializeField] private bool test;

  private KitchenObject kitchenObject;

  private void Update() {
    if (test && Input.GetKeyDown(KeyCode.Y)) {
      if (GetKitchenObject() != null) {
        GetKitchenObject().ClearCounter = otherCounter.GetComponent<ClearCounter>();
      }
    }
  }

  public void Interact() {
    if (GetKitchenObject() != null) {
      return;
    }

    var kitchenObjectTransform = Instantiate(kitchenObjectSO.Prefab, GetKitchenObjectFollowTransform());
    kitchenObjectTransform.GetComponent<KitchenObject>().ClearCounter = this;
  }

  public KitchenObject GetKitchenObject() => kitchenObject;
  public void SetKitchenObject(KitchenObject aKitchenObject) => kitchenObject = aKitchenObject;
  public void ClearKitchenObject() => kitchenObject = null;
  public bool GetHasKitchenObject() => GetKitchenObject() != null;
  public Transform GetKitchenObjectFollowTransform() => counterTopPoint;
}
