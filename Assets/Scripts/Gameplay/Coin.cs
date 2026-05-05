using System;
using TMPro;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public static event Action<double> OnCoinPickup;

    [SerializeField] private GameObject popupAmountPrefab;
    [SerializeField] private double value = 0;

    private bool isDead = false;

    void Start()
    {
        if (StatsManager.instance != null)
        {
            Stat goldIncome = StatsManager.instance.GetStat(StatType.GOLD_INCOME);

            value += goldIncome.GetValue();
        }
    }

    void OnMouseOver()
    {
        //Debug.Log($"OnMouseOver {gameObject.name}");

        if (isDead) return;

        isDead = true;

        PlaySFX();

        ShowAmountPopup();

        OnCoinPickup?.Invoke(value);

        Destroy(gameObject);
    }

    void PlaySFX()
    {
        AudioManager.instance.PlayFX(AudioType.SFX, "coin");
    }

    void ShowAmountPopup()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        GameObject clone = Instantiate(popupAmountPrefab, mousePos, Quaternion.identity);
        TMP_Text amountText = clone.GetComponentInChildren<TMP_Text>();

        amountText.text = "" + value;
        amountText.color = Color.yellow;
    }
}