using System;
using TMPro;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public static event Action<double> OnCoinPickup;

    [SerializeField] private GameObject popupAmountPrefab;
    [SerializeField] private StatType scalesWithStat;
    [SerializeField] private double baseValue = 0;
    [SerializeField] private AudioClip pickupSfx;

    private bool isDead = false;

    void Start()
    {
        if (StatsManager.instance != null)
        {
            Stat stat = StatsManager.instance.GetStat(scalesWithStat);

            baseValue += stat.GetValue();
        }
    }

    void OnMouseOver()
    {
        if (isDead) return;

        isDead = true;

        PlaySFX();

        ShowAmountPopup();

        OnCoinPickup?.Invoke(baseValue);

        Destroy(gameObject);
    }

    void PlaySFX()
    {
        AudioManager.instance.PlayFX(pickupSfx);
    }

    void ShowAmountPopup()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        GameObject clone = Instantiate(popupAmountPrefab, mousePos, Quaternion.identity);
        TMP_Text amountText = clone.GetComponentInChildren<TMP_Text>();

        amountText.text = "" + baseValue;
        amountText.color = Color.yellow;
    }
}