using System.Collections.Generic;
using UnityEngine;

public class FlaskManager : MonoBehaviour
{
    public static FlaskManager instance = null;

    [SerializeField] List<Ingredient> flaskIngredients = new();

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

    // TODO: Bug if same ingredient enters trigger multiple times

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

        SpawnPotion(recipe);

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

    private void SpawnPotion(RecipeData recipe)
    {
        Vector3 position = new Vector3(2.5f, 5f, 0f);

        int counter = 1;
        for (int i = 0; i < counter; i++)
        {
            GameObject clone = Instantiate(recipe.resultPotion.prefab, position, Quaternion.identity);
            Potion potion = clone.GetComponent<Potion>();
            potion.Init(recipe.resultPotion);
        }
    }
}
