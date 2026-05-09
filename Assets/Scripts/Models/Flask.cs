using System.Collections.Generic;
using UnityEngine;

public class Flask : MonoBehaviour
{
    [SerializeField] List<Ingredient> ingredients = new();

    [SerializeField] private int ingredientSlots = 3;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Ingredient ingredient = collision.gameObject.GetComponent<Ingredient>();

        if (ingredients.Count < ingredientSlots)
        {
            ingredients.Add(ingredient);
        }
    }

    private void OnMouseDown()
    {
        if (ingredients.Count > 0)
        {
            BrewPotion();
        }
    }

    private void BrewPotion()
    {
        List<IngredientData> list = new();

        foreach (Ingredient ingredient in ingredients)
        {
            list.Add(ingredient.Data);
        }

        PotionData result = RecipeManager.instance.GetResult(list);

        if (result == null) return;

        GameObject clone = Instantiate(result.prefab, Vector3.zero, Quaternion.identity);
        Potion potion = clone.GetComponent<Potion>();
        potion.Init(result);

        foreach (Ingredient ingredient in ingredients)
        {
            Destroy(ingredient.gameObject);
        }

        ingredients.Clear();
    }
}
