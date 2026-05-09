using UnityEngine;

public class Ingredient : MonoBehaviour
{
    private IngredientData data;

    public IngredientData Data => data;

    public void Init(IngredientData ingredientData)
    {
        data = ingredientData;
    }
}