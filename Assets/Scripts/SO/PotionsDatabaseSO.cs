using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "LootIdle/Create Potions Database...")]
public class PotionsDatabaseSO : ScriptableObject
{
    [SerializeField]
    public List<PotionData> allPotions;

    public PotionData GetEntry(string label)
    {
        foreach (PotionData potion in allPotions)
        {
            if (potion.label == label)
            {
                return potion;
            }
        }

        return null;
    }
}