using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent {
  [SerializeField] private Transform counterTopPoint;

  private KitchenObject kitchenObject;

  public void ClearKitchenObject() => kitchenObject = null;

  public KitchenObject GetKitchenObject() => kitchenObject;

  public Transform GetKitchenObjectFollowTransform() => counterTopPoint;

  public bool HasKitchenObject() => GetKitchenObject() != null;

  public virtual void Interact(Player aPlayer) {
    Debug.LogError("BaseCounter.Interact()");
  }

  public virtual void InteractAlternative(Player player) {
  }

  public void SetKitchenObject(KitchenObject aKitchenObject) => kitchenObject = aKitchenObject;
  public static bool IsHoldingAPlate(IKitchenObjectParent aKitchenObjectParent) {
    return aKitchenObjectParent.HasKitchenObject() && aKitchenObjectParent.GetKitchenObject() is PlateKitchenObject;
  }
}