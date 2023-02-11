using UnityEngine;

[CreateAssetMenu(menuName = "SO/Recipe/Frying")]
public class FryingRecipeSO : ScriptableObject {
  [field: SerializeField] public KitchenObjectSO Input { get; private set; }

  [field: SerializeField] public KitchenObjectSO Output { get; private set; }
  [field: SerializeField] public float FryingTimerMax { get; private set; }
}
