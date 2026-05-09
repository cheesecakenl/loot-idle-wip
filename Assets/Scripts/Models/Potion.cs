using System;
using UnityEngine;

public class Potion : MonoBehaviour
{
    public static event Action<string> OnPotionPickup;

    [SerializeField] private PotionData data;

    [SerializeField] private AudioClip pickupSfx;

    private bool isPickedUp = false;

    void OnMouseOver()
    {
        if (isPickedUp) return;

        isPickedUp = true;

        PlaySFX();

        OnPotionPickup?.Invoke(data.label);

        Destroy(gameObject);
    }

    void PlaySFX()
    {
        AudioManager.instance.PlayFX(pickupSfx);
    }
}
