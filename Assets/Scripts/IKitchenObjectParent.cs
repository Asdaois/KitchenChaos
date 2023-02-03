using UnityEngine;

public interface IKitchenObjectParent {
  public KitchenObject GetKitchenObject();
  public void SetKitchenObject(KitchenObject aKitchenObject);
  public void ClearKitchenObject();
  public bool GetHasKitchenObject();
  public Transform GetKitchenObjectFollowTransform();
}
