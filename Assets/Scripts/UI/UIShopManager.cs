using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIShopManager : MonoBehaviour
{
    [SerializeField] private Transform contentParent;

    [SerializeField] private GameObject itemPlaceholder;

    [SerializeField] private IngredientDatabaseSO ingredientDatabase;

    void OnEnable()
    {
        GameEvents.Money.OnMoneyChanged += HandleOnMoneyChanged;
    }

    void OnDisable()
    {
        GameEvents.Money.OnMoneyChanged -= HandleOnMoneyChanged;
    }

    private void Start()
    {
        UpdateAffordableIngredients();
    }

    private void UpdateAffordableIngredients()
    {
        List<IngredientData> affordableIngredients = new();

        double currentMoney = PlayerManager.instance.money;

        foreach (IngredientData ingredient in ingredientDatabase.allIngredients)
        {
            Stat stat = StatsManager.instance.GetStat(ingredient.costStatType);
            double cost = ingredient.baseCost + stat.GetValue();

            if (cost <= currentMoney)
            {
                affordableIngredients.Add(ingredient);
            }
        }

        ClearShop();

        ShowIngredients(affordableIngredients);
    }

    private void ClearShop()
    {
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }
    }

    private void ShowIngredients(List<IngredientData> ingredients)
    {
        if (ingredients.Count < 1) return;

        foreach (IngredientData ingredient in ingredients)
        {
            GameObject clone = Instantiate(itemPlaceholder, contentParent);

            Image icon = clone.transform.Find("Icon").GetComponent<Image>();
            Button buyButton = clone.transform.Find("BuyButton").GetComponent<Button>();
            TMP_Text buttonText = buyButton.GetComponentInChildren<TMP_Text>();

            Stat stat = StatsManager.instance.GetStat(ingredient.costStatType);
            double cost = ingredient.baseCost + stat.GetValue();

            buyButton.onClick.RemoveAllListeners();
            buyButton.onClick.AddListener(() => BuyIngredient(ingredient.label, cost));

            icon.sprite = ingredient.uiIcon;
            buttonText.text = MoneyHelper.FormatMoney(cost);
        }
    }

    private void BuyIngredient(string label, double cost)
    {
        IngredientData data = ingredientDatabase.GetIngredient(label);

        GameObject clone = Instantiate(data.prefab, Vector3.zero, Quaternion.identity);
        Ingredient ingredient = clone.GetComponent<Ingredient>();
        ingredient.Init(data);

        PlayerManager.instance.Pay(cost);
    }

    private void HandleOnMoneyChanged(double amount)
    {
        UpdateAffordableIngredients();
    }
}
