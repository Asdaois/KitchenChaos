using UnityEngine;

public class DeliveryCounter : BaseCounter {
  public static DeliveryCounter Instance { get; private set; }

  private void Awake() {
    if (Instance != null) {
      Debug.LogError("An instance of Delivery Counter exist this is a bug");
    }

    Instance = this;
  }
  public override void Interact(Player aPlayer) {
    if (IsHoldingAPlate(aPlayer)
        && aPlayer.GetKitchenObject().TryGetPlate(out var plate)) {
      DeliveryManager.Instance.Delivery(plate);
      plate.DestroySelf();
    }
  }
}
