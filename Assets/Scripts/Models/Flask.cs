using System.Collections.Generic;
using UnityEngine;

public class Flask : MonoBehaviour
{
    [SerializeField] List<Ingredient> flaskIngredients = new();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Ingredient ingredient = collision.gameObject.GetComponent<Ingredient>();

        flaskIngredients.Add(ingredient);
    }

    private void OnMouseDown()
    {
        if (flaskIngredients.Count > 0)
        {
            BrewPotion();
        }
    }

    private void BrewPotion()
    {
        List<IngredientData> brewIngredients = new();

        foreach (Ingredient ingredient in flaskIngredients)
        {
            brewIngredients.Add(ingredient.Data);
        }

        RecipeData recipe = RecipeManager.instance.GetResult(brewIngredients);

        if (recipe == null) return;

        GameObject clone = Instantiate(recipe.resultPotion.prefab, Vector3.zero, Quaternion.identity);
        Potion potion = clone.GetComponent<Potion>();
        potion.Init(recipe.resultPotion);

        // Remove ingredients
        List<string> labels = new();

        foreach (IngredientData data in recipe.ingredients)
        {
            labels.Add(data.label);
        }

        foreach (string label in labels)
        {
            for (int i = 0; i < flaskIngredients.Count; i++)
            {
                Ingredient ingredient = flaskIngredients[i];

                if (ingredient.Data.label == label)
                {
                    Destroy(ingredient.gameObject);
                    flaskIngredients.RemoveAt(i);

                    break;
                }
            }
        }
    }
}
