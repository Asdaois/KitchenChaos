
using System;
using UnityEngine;

public class PlateCounter : BaseCounter {
  private const float SPAWN_PLATE_TIMER_MAX = 4f;
  private const int SPAWN_PLATE_MAX = 4;

  public event EventHandler OnPlateSpawn;
  public event EventHandler OnPlateRemove;

  [SerializeField] private KitchenObjectSO plateKitchenObject;
  private float spawnPlateTimer;
  private int spawnedPlates;

  private void Update() {
    HandlePlateSpawn();
  }

  private void HandlePlateSpawn() {
    if (spawnedPlates > SPAWN_PLATE_MAX)
      return;

    if (spawnPlateTimer <= SPAWN_PLATE_TIMER_MAX) {
      spawnPlateTimer += Time.deltaTime;
      return;
    }

    spawnedPlates++;
    spawnPlateTimer = 0;
    OnPlateSpawn?.Invoke(this, EventArgs.Empty);
  }

  public override void Interact(Player aPlayer) {
    if (aPlayer.HasKitchenObject()) {
      return;
    }

    if (spawnedPlates == 0) {
      return;
    }

    spawnedPlates--;
    KitchenObject.Spawn(plateKitchenObject, aPlayer);
    OnPlateRemove?.Invoke(this, EventArgs.Empty);
  }
}
