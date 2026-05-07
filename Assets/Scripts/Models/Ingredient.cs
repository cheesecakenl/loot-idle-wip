using System;
using TMPro;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    public static event Action<double> OnIngredientDrop;

    [SerializeField] private GameObject popupAmountPrefab;
    [SerializeField] private StatType scalesWithStat;
    [SerializeField] private double baseValue = 0;
    [SerializeField] private AudioClip dropSfx;

    private bool isDead = false;

    void Start()
    {
        if (StatsManager.instance != null)
        {
            Stat stat = StatsManager.instance.GetStat(scalesWithStat);

            baseValue += stat.GetValue();
        }
    }

    public void DoDrop()
    {
        if (isDead) return;

        isDead = true;

        PlaySFX();

        ShowAmountPopup();

        OnIngredientDrop?.Invoke(baseValue);

        Destroy(gameObject);
    }

    void PlaySFX()
    {
        AudioManager.instance.PlayFX(dropSfx);
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