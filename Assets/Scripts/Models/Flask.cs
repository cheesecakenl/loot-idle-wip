using System.Collections.Generic;
using UnityEngine;

public class Flask : MonoBehaviour
{
    [SerializeField] List<Ingredient> ingredients = new();

    [SerializeField] private int ingredientSlots = 1;

    [SerializeField] private PotionsDatabaseSO potionsDatabase;

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
        PotionData data = potionsDatabase.GetPotion("Potion A");

        GameObject clone = Instantiate(data.prefab, Vector3.zero, Quaternion.identity);
        Potion potion = clone.GetComponent<Potion>();
        potion.Init(data);

        foreach (Ingredient ingredient in ingredients)
        {
            Destroy(ingredient.gameObject);
        }

        ingredients.Clear();
    }
}
