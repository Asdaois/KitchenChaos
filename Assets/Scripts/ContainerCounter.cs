using System;
using UnityEngine;

public class ContainerCounter : BaseCounter {

  public event EventHandler OnPlayerGrabObject;

  [SerializeField] private KitchenObjectSO kitchenObjectSO;

  public override void Interact(Player aPlayer) {
    if (aPlayer.HasKitchenObject()) {
      return;
    }

    KitchenObject.Spawn(kitchenObjectSO, aPlayer);
    OnPlayerGrabObject?.Invoke(this, EventArgs.Empty);
  }
}