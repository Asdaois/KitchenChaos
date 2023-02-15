using System;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour {
  public event EventHandler OnWaitingRecipeChange;
  public event EventHandler OnRecipeSuccess;
  public event EventHandler OnRecipeFailed;

  public static DeliveryManager Instance { get; private set; }
  [SerializeField] private RecipeListSO recipesSO;

  [SerializeField] private Timer spawRecipeTimer;

  private List<RecipeSO> waitingRecipes = new();
  private int succesfulRecipesDelivered;
  private const int waitingRecipesMax = 4;


  private void Awake() {
    Instance = this;
  }

  private void Start() {
    spawRecipeTimer.OnTimeup += SpawnRecipe;
  }

  private void SpawnRecipe(object sender, EventArgs e) {
    if (waitingRecipes.Count >= waitingRecipesMax)
      return;

    int randomIndex = UnityEngine.Random.Range(0, recipesSO.GetRecipes().Count);
    var randomRecipe = recipesSO.GetRecipes()[randomIndex];
    waitingRecipes.Add(randomRecipe);
    OnRecipeChanged();
  }

  public void Delivery(PlateKitchenObject aPlate) {
    var ingredientsInPlate = new HashSet<KitchenObjectSO>(aPlate.GetIngredients());

    foreach (var recipe in waitingRecipes) {
      var ingredientsInWaitingRecipe = new HashSet<KitchenObjectSO>(recipe.GetIngredients());

      var isSameNumberOfIngredients = ingredientsInWaitingRecipe.Count == ingredientsInPlate.Count;
      var hasSameValues = ingredientsInWaitingRecipe.IsSubsetOf(ingredientsInPlate);
      var areSameIngredients = isSameNumberOfIngredients && hasSameValues;

      if (areSameIngredients) {
        Debug.Log($"This recipe was delivered: {recipe}");
        waitingRecipes.Remove(recipe);
        OnRecipeChanged();
        OnRecipeSuccess?.Invoke(this, new());
        succesfulRecipesDelivered += 1;
        return; //! Unsafe if your delete this return
      }
    }

    OnRecipeFailed?.Invoke(this, new());
  }

  private void OnRecipeChanged() {
    OnWaitingRecipeChange?.Invoke(this, EventArgs.Empty);
  }

  public List<RecipeSO> GetWaitingRecipes() {
    return waitingRecipes;
  }

  public int GetRecipesDeliveredAmount() {
    return succesfulRecipesDelivered;
  }
}
