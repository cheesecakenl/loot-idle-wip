using UnityEngine;

[CreateAssetMenu(menuName = "LootIdle/Create Potion Data...")]
public class PotionData : GameData
{
    public int tier;
    public GameObject prefab;
    public double baseValue;
    public AudioClip pickupSfx;
}