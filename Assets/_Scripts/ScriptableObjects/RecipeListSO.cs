using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Collections/Recipes")]
public class RecipeListSO : ScriptableObject {

  [SerializeField] private List<RecipeSO> recipes;
  public List<RecipeSO> GetRecipes() {
    return recipes;
  }
}
