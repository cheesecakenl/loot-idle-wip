using UnityEngine;

public class Potion : MonoBehaviour
{
    [SerializeField] public PotionData data;

    void OnMouseOver()
    {
        PlaySFX();

        GameEvents.Potion.OnPotionPickup?.Invoke(gameObject);
    }

    void PlaySFX()
    {
        AudioManager.instance.PlayFX(data.pickupSfx);
    }
}
