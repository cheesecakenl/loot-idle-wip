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

        foreach (IngredientData ingredienDatat in ingredientDatabase.allIngredients)
        {
            double costStat = StatsManager.instance.GetValue(
                ingredienDatat,
                StatType.INGREDIENT_BUY_DISCOUNT,
                ingredienDatat.baseCost
            );

            double cost = ingredienDatat.baseCost + costStat;

            if (cost <= currentMoney)
            {
                affordableIngredients.Add(ingredienDatat);
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

            double discount = StatsManager.instance.GetValue(
                ingredientData,
                StatType.INGREDIENT_BUY_DISCOUNT,
                ingredientData.baseCost
            );

            double costs = ingredientData.baseCost - discount;

            uiShopItem.buyButton.onClick.RemoveAllListeners();
            uiShopItem.buyButton.onClick.AddListener(() => BuyIngredient(ingredientData.label, costs));

            uiShopItem.icon.sprite = ingredientData.uiIcon;
            uiShopItem.buyButtonText.text = MoneyHelper.FormatMoney(costs);
        }
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
