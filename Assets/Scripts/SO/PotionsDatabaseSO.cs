using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "LootIdle/Create Potions Database...")]
public class PotionsDatabaseSO : ScriptableObject
{
    [SerializeField]
    public List<PotionData> allPotions;

    public PotionData GetPotion(string label)
    {
        foreach (PotionData data in allPotions)
        {
            if (data.label == label)
            {
                return data;
            }
        }

        return null;
    }
}