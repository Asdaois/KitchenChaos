using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour {
  [SerializeField] private List<KitchenGameObjectSO_GameObject> kitchenGameObjectSO_GameObjects;
  [SerializeField] private PlateKitchenObject plateKitchenObject;

  private void Start() {
    plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;

    kitchenGameObjectSO_GameObjects.ForEach(kitchenGameObject => kitchenGameObject.GameObject.SetActive(false));
  }

  private void PlateKitchenObject_OnIngredientAdded(object sender, OnIngredientAddedEventArgs e) {
    var kitchen_gameObject = kitchenGameObjectSO_GameObjects.Find(kitchen => kitchen.kitchenObjectSO == e.ingredient);
    kitchen_gameObject.GameObject.SetActive(true);
  }
  [Serializable]
  public struct KitchenGameObjectSO_GameObject {
    public GameObject GameObject;
    public KitchenObjectSO kitchenObjectSO;
  }
}
