using UnityEngine;
using UnityEngine.UI;

public class PlateIconUI : MonoBehaviour {
  [SerializeField] private Image image;
  KitchenObjectSO kitchenObjectSO;
  public void SetKitchenObjectSO(KitchenObjectSO aKitchenObjectSO) {
    kitchenObjectSO = aKitchenObjectSO;
    image.sprite = kitchenObjectSO.Sprite;
  }
}
