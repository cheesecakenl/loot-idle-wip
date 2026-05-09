using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "LootIdle/Create Recipe Data...")]
public class RecipeData : ScriptableObject
{
    public string label;
    public List<IngredientData> ingredients;
    public PotionData resultPotion;
}