using System;
using UnityEngine;

public class Potion : MonoBehaviour
{
    public static event Action<GameObject> OnPotionPickup;

    [SerializeField] public PotionData data;

    void OnMouseOver()
    {
        PlaySFX();

        OnPotionPickup?.Invoke(gameObject);
    }

    void PlaySFX()
    {
        AudioManager.instance.PlayFX(data.pickupSfx);
    }
}
