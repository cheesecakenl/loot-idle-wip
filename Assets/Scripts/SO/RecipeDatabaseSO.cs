using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "LootIdle/Create Recipe Database...")]
public class RecipeDatabaseSO : ScriptableObject
{
    [SerializeField]
    public List<RecipeData> allRecipes;

    public RecipeData GetRecipe(string label)
    {
        foreach (RecipeData data in allRecipes)
        {
            if (data.label == label)
            {
                return data;
            }
        }

        return null;
    }
}