using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent {
  [SerializeField] private Transform counterTopPoint;

  private KitchenObject kitchenObject;

  public virtual void Interact(Player aPlayer) => Debug.LogError("BaseCounter.Interact()");

  public KitchenObject GetKitchenObject() => kitchenObject;

  public void SetKitchenObject(KitchenObject aKitchenObject) => kitchenObject = aKitchenObject;

  public void ClearKitchenObject() => kitchenObject = null;

  public bool HasKitchenObject() => GetKitchenObject() != null;

  public Transform GetKitchenObjectFollowTransform() => counterTopPoint;
}