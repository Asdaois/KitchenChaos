using UnityEngine;

public interface IKitchenObjectParent {
  public KitchenObject GetKitchenObject();
  public void SetKitchenObject(KitchenObject aKitchenObject);
  public void ClearKitchenObject();
  public bool HasKitchenObject();
  public Transform GetKitchenObjectFollowTransform();
}
