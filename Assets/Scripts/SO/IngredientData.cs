using UnityEngine;

[CreateAssetMenu(menuName = "LootIdle/Create Ingredient Data...")]
public class IngredientData : GameData
{
    public int tier;
    public GameObject prefab;
    public double baseCost;
    public AudioClip dropSfx;
}