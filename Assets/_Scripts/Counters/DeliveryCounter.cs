public class DeliveryCounter : BaseCounter {

  public override void Interact(Player aPlayer) {
    if (IsHoldingAPlate(aPlayer)) {
      aPlayer.GetKitchenObject().TryGetPlate(out var plate);
      plate.DestroySelf();
    }
  }
}
