using System;

public class TrashCounter : BaseCounter {

  public static event EventHandler OnAnyObjectTrashed;
  public override void Interact(Player aPlayer) {
    if (aPlayer.HasKitchenObject()) {
      aPlayer.GetKitchenObject().DestroySelf();
      OnAnyObjectTrashed?.Invoke(this, EventArgs.Empty);
    }
  }
}
