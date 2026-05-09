using UnityEngine;

public class Potion : MonoBehaviour
{
    private PotionData data;

    public PotionData Data => data;

    public void Init(PotionData data)
    {
        this.data = data;
    }

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
