using System;
using UnityEngine;

public class ContainerCounter : BaseCounter {

  public event EventHandler OnPlayerGrabObject;

  [SerializeField] private KitchenObjectSO kitchenObjectSO;

  public override void Interact(Player aPlayer) {
    var kitchenObjectTransform = Instantiate(kitchenObjectSO.Prefab);
    kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjepctParent(aPlayer);
    OnPlayerGrabObject?.Invoke(this, EventArgs.Empty);
  }
}