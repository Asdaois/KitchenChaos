using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Recipe/Delivery")]
public class RecipeSO : ScriptableObject {
  [SerializeField] private List<KitchenObjectSO> ingredients;
  [SerializeField] private string recipeName;

  public List<KitchenObjectSO> GetIngredients() {
    return ingredients;
  }

  public string GetRecipeName() {
    return recipeName;
  }

}
