using UnityEngine;

public class ContainerCounter : BaseCounter, IKitchenObjectParent {
  [SerializeField] private KitchenObjectSO kitchenObjectSO;
  [SerializeField] private Transform counterTopPoint;

  private KitchenObject kitchenObject;

  public override void Interact(Player aPlayer) {
    if (GetKitchenObject() != null) {
      kitchenObject.SetKitchenObjepctParent(aPlayer);
      return;
    }

    var kitchenObjectTransform = Instantiate(kitchenObjectSO.Prefab, GetKitchenObjectFollowTransform());
    kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjepctParent(this);
  }

  public KitchenObject GetKitchenObject() => kitchenObject;
  public void SetKitchenObject(KitchenObject aKitchenObject) => kitchenObject = aKitchenObject;
  public void ClearKitchenObject() => kitchenObject = null;
  public bool GetHasKitchenObject() => GetKitchenObject() != null;
  public Transform GetKitchenObjectFollowTransform() => counterTopPoint;

}
