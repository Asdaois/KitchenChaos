using UnityEngine;

[CreateAssetMenu(menuName = "SO/Recipe/Burning")]
public class BurnigRecipeSO : ScriptableObject {
  [field: SerializeField] public KitchenObjectSO Input { get; private set; }
  [field: SerializeField] public KitchenObjectSO Output { get; private set; }
  [field: SerializeField] public float FryingTimerMax { get; private set; }
}
