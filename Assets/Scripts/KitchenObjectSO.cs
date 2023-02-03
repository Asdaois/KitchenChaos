using UnityEngine;

[CreateAssetMenu(menuName = "SO/KitchenObject", fileName = "KitchenObject")]
public class KitchenObjectSO : ScriptableObject {
  [field: SerializeField]
  public string Name { get; private set; }
  [field: SerializeField]
  public Transform Prefab { get; private set; }
  [field: SerializeField]
  public Sprite Sprite { get; private set; }
}
