using UnityEngine;

public class ClearCounter : MonoBehaviour {
  [SerializeField] private KitchenObjectSO kitchenObjectSO;
  [SerializeField] private Transform counterTopPoint;
  public void Interact() {
    Debug.Log("Interaact");
    var kitchenObjectTransform = Instantiate(kitchenObjectSO.Prefab, counterTopPoint);
    kitchenObjectTransform.localPosition = Vector3.zero;

    Debug.Log(kitchenObjectTransform.GetComponent<KitchenObject>()?.KitchenObjectSO.Name);
  }
}
