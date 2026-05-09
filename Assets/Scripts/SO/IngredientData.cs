using UnityEngine;

[CreateAssetMenu(menuName = "LootIdle/Create Ingredient Data...")]
public class IngredientData : ScriptableObject
{
    public string label;
    public int tier;
    public GameObject prefab;
    public Sprite uiIcon;

    [SerializeField, Tooltip("This upgrade effects the chosen stat")]
    public StatType costStatType;

    public double baseCost;

    public AudioClip dropSfx;
}