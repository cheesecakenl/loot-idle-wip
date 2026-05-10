using System;
using System.Collections.Generic;
using UnityEngine;

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

        foreach (IngredientData ingredienData in ingredientDatabase.allIngredients)
        {
            double cost = GetIngredientPrice(ingredienData);

            if (cost <= currentMoney)
            {
                affordableIngredients.Add(ingredienData);
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

        foreach (IngredientData ingredientData in ingredients)
        {
            GameObject clone = Instantiate(itemPlaceholder, contentParent);
            UIShopItem uiShopItem = clone.GetComponent<UIShopItem>();

            double costs = GetIngredientPrice(ingredientData);

            uiShopItem.buyButton.onClick.RemoveAllListeners();
            uiShopItem.buyButton.onClick.AddListener(() => BuyIngredient(ingredientData.label, costs));

            uiShopItem.icon.sprite = ingredientData.uiIcon;
            uiShopItem.buyButtonText.text = MoneyHelper.FormatMoney(costs);
        }
    }

    private double GetIngredientPrice(IngredientData ingredientData)
    {
        double discount = StatsManager.instance.GetValue(
            ingredientData,
            StatType.INGREDIENT_BUY_DISCOUNT,
            0
            );

        double costs = ingredientData.baseCost - discount;

        return Math.Max(0, costs);
    }

    private void BuyIngredient(string label, double cost)
    {
        IngredientData data = ingredientDatabase.GetIngredient(label);

        SpawnIngredient(data);

        PlayerManager.instance.Pay(cost);
    }

    private void SpawnIngredient(IngredientData data)
    {
        GameObject clone = Instantiate(data.prefab, Vector3.zero, Quaternion.identity);
        Ingredient ingredient = clone.GetComponent<Ingredient>();
        ingredient.Init(data);
    }

    private void HandleOnMoneyChanged(double amount)
    {
        UpdateAffordableIngredients();
    }
}
