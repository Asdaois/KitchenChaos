using UnityEngine;

[CreateAssetMenu(menuName = "SO/Recipe/Cutting")]
public class CuttingRecipeSO : ScriptableObject {
  [field: SerializeField] public KitchenObjectSO Input { get; private set; }

  [field: SerializeField] public KitchenObjectSO Output { get; private set; }
  [field: SerializeField] public int CuttingProgresMaximum { get; private set; }
}