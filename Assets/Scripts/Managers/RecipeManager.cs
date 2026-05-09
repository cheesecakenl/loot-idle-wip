using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RecipeManager : MonoBehaviour
{
    public static RecipeManager instance = null;

    [SerializeField] private RecipeDatabaseSO recipeDatabase;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        //DontDestroyOnLoad(gameObject);
    }

    public PotionData GetResult(List<IngredientData> inputIngredients)
    {
        foreach (RecipeData recipe in recipeDatabase.allRecipes)
        {
            if (MatchesRecipe(inputIngredients, recipe.ingredients))
            {
                return recipe.resultPotion;
            }
        }

        return null;
    }

    private bool MatchesRecipe(
        List<IngredientData> input,
        List<IngredientData> recipe)
    {
        if (input.Count != recipe.Count) return false;

        return !input.Except(recipe).Any() && !recipe.Except(input).Any();
    }
}
