
using UnityEngine;

public class PlateCounter : BaseCounter {
  private const float SPAWN_PLATE_TIMER_MAX = 4f;

  [SerializeField] private KitchenObjectSO plateKitchenObject;
  private float spawnPlateTimer;

  private void Update() {
    spawnPlateTimer += Time.deltaTime;
    if (spawnPlateTimer > SPAWN_PLATE_TIMER_MAX) {
      KitchenObject.Spawn(plateKitchenObject, this);
    }
  }
}
