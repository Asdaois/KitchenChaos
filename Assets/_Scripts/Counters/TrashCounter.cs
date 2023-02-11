public class TrashCounter : BaseCounter {
  public override void Interact(Player aPlayer) {
    if (aPlayer.HasKitchenObject()) {
      aPlayer.GetKitchenObject().DestroySelf();
    }
  }
}
