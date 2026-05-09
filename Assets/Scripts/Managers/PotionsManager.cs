using System;
using UnityEngine;

public class PotionsManager : MonoBehaviour
{
    [SerializeField] private PotionsDatabaseSO potionsDatabase;

    void OnEnable()
    {
        Potion.OnPotionPickup += HandleOnPotionPickup;
    }

    void OnDisable()
    {
        Potion.OnPotionPickup -= HandleOnPotionPickup;
    }

    private void HandleOnPotionPickup(string potionLabel)
    {
        
    }
}
