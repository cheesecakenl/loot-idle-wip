using UnityEngine;

[CreateAssetMenu(menuName = "LootIdle/Create Potion Data...")]
public class PotionData : ScriptableObject
{
    public string label;
    public int tier;
    public GameObject prefab;
    public Sprite uiIcon;

    [SerializeField, Tooltip("This upgrade effects the chosen stat")]
    public StatType valueStatType;

    public double baseValue;

    public AudioClip pickupSfx;
}