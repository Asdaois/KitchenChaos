using System;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour {
  [SerializeField] private RecipeListSO recipesSO;

  [SerializeField] private Timer spawRecipeTimer;

  private List<RecipeSO> waitingRecipes = new();

  private const int waitingRecipesMax = 4;

  private void Start() {
    spawRecipeTimer.OnTimeup += SpawnRecipe;
  }

  private void SpawnRecipe(object sender, EventArgs e) {
    if (waitingRecipes.Count >= waitingRecipesMax)
      return;

    int randomIndex = UnityEngine.Random.Range(0, recipesSO.GetRecipes().Count);
    var randomRecipe = recipesSO.GetRecipes()[randomIndex];
    Debug.Log(randomRecipe.GetRecipeName());
    waitingRecipes.Add(randomRecipe);
  }
}
