using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "LootIdle/Create Ingredients Database...")]
public class IngredientDatabaseSO : ScriptableObject
{
    [SerializeField]
    public List<IngredientData> allIngredients;

    public IngredientData GetIngredient(string label)
    {
        foreach (IngredientData data in allIngredients)
        {
            if (data.label == label)
            {
                return data;
            }
        }

        return null;
    }
}