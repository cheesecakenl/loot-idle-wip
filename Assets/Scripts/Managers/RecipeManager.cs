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
        RecipeData recipeData = GetBestRecipeMatch(inputIngredients);

        return recipeData != null ? recipeData.resultPotion : null;
    }

    private bool MatchesRecipe(List<IngredientData> input, List<IngredientData> recipe)
    {
        var inputCounts = input
            .GroupBy(i => i)
            .ToDictionary(g => g.Key, g => g.Count());

        var recipeCounts = recipe
            .GroupBy(i => i)
            .ToDictionary(g => g.Key, g => g.Count());

        foreach (var pair in recipeCounts)
        {
            IngredientData ingredient = pair.Key;
            int requiredAmount = pair.Value;

            if (!inputCounts.TryGetValue(ingredient, out int inputAmount))
                return false;

            if (inputAmount < requiredAmount)
                return false;
        }

        return true;
    }

    public RecipeData GetBestRecipeMatch(List<IngredientData> input)
    {
        RecipeData bestMatch = null;
        int bestScore = -1;

        foreach (RecipeData recipe in recipeDatabase.allRecipes)
        {
            if (!MatchesRecipe(input, recipe.ingredients))
                continue;

            int score = recipe.ingredients.Count;

            if (score > bestScore)
            {
                bestScore = score;
                bestMatch = recipe;
            }
        }

        return bestMatch;
    }
}
