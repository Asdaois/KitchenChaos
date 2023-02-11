using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlateCounterVisual : MonoBehaviour {
  [SerializeField] private Transform counterTopPoint;
  [SerializeField] private Transform plateVisualPrefab;
  [SerializeField] private PlateCounter plateCounter;

  List<GameObject> plates = new();

  private void Start() {
    plateCounter.OnPlateSpawn += PlateCounter_OnPlateSpawn;
    plateCounter.OnPlateRemove += PlateCounter_OnPlateRemove;
  }

  private void PlateCounter_OnPlateRemove(object sender, System.EventArgs e) {
    var lastPlate = plates.Last();
    plates.Remove(lastPlate);
    Destroy(lastPlate);
  }

  private void PlateCounter_OnPlateSpawn(object sender, System.EventArgs e) {
    var plateVisualTransform = Instantiate(plateVisualPrefab, counterTopPoint);

    var plateOffsetY = 0.1f;
    plateVisualTransform.localPosition = new(0, plateOffsetY * plates.Count, 0);

    plates.Add(plateVisualTransform.gameObject);
  }
}
