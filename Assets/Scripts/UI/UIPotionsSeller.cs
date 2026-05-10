using System.Collections.Generic;
using UnityEngine;

public class UIPotionsSeller : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private Transform contentParent;
    [SerializeField] private GameObject itemPlaceholder;

    [SerializeField] private float sellTimerInterval = 3f;
    private float sellTimer;
    private float sellTimerImageWidth;

    [SerializeField] private int maxPotionTypes = 3;
    [SerializeField] private List<PotionSellData> potionsToSell = new();

    [SerializeField] private AudioClip sellSfx;

    void OnEnable()
    {
        GameEvents.Potion.OnPotionPickup += HandleOnPotionPickup;
    }

    void OnDisable()
    {
        GameEvents.Potion.OnPotionPickup -= HandleOnPotionPickup;
    }

    private void Awake()
    {
        sellTimerImageWidth = rectTransform.rect.width;
    }

    private void Start()
    {
        ClearShop();
    }

    private void ClearShop()
    {
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }
    }

    private void AddPotion(PotionData data)
    {
        bool potionAlreadySold = false;
        foreach (PotionSellData sellData in potionsToSell)
        {
            if (sellData.potionData.label == data.label)
            {
                potionAlreadySold = true;
                sellData.amount += 1;
            }
        }

        if (!potionAlreadySold)
        {
            PotionSellData sellData = new PotionSellData();
            sellData.potionData = data;
            sellData.amount = 1;

            potionsToSell.Add(sellData);
        }
    }

    private void HandleOnPotionPickup(GameObject go)
    {
        if (potionsToSell.Count < maxPotionTypes)
        {
            PotionData data = go.GetComponent<Potion>().Data;

            AddPotion(data);

            ClearShop();

            ShowPotions();

            Destroy(go);
        }
    }

    private void ShowPotions()
    {
        if (potionsToSell.Count < 1) return;

        foreach (PotionSellData sellData in potionsToSell)
        {
            GameObject clone = Instantiate(itemPlaceholder, contentParent);
            UISellPotionSlot uiShopItem = clone.GetComponent<UISellPotionSlot>();

            uiShopItem.icon.sprite = sellData.potionData.uiIcon;
            uiShopItem.amountText.text = MoneyHelper.FormatMoney(sellData.amount);
        }
    }

    private void Update()
    {
        if (sellTimer > sellTimerInterval)
        {
            Sell();

            ResetVisualTimer();

            sellTimer = 0;
        }

        if (potionsToSell.Count > 0)
        {
            DecreaseVisualTimer();
            sellTimer += Time.deltaTime;
        }
    }

    private void DecreaseVisualTimer()
    {
        float percent = Mathf.Clamp01(sellTimer / sellTimerInterval);

        rectTransform.sizeDelta = new Vector2(
            sellTimerImageWidth * (1f - percent),
            rectTransform.sizeDelta.y
        );
    }

    private void ResetVisualTimer()
    {
        rectTransform.sizeDelta = new Vector2(sellTimerImageWidth, rectTransform.rect.height);
    }

    private void Sell()
    {
        if (potionsToSell.Count > 0)
        {
            PotionSellData sellData = potionsToSell[0];

            double sellValue = StatsManager.instance.GetValue(
                sellData.potionData,
                StatType.POTION_SELL_VALUE,
                sellData.potionData.baseValue
            );

            //Stat stat = StatsManager.instance.GetStat(sellData.potionData.valueStatType);
            //double amount = sellData.potionData.baseValue + stat.GetValue();

            AudioManager.instance.PlayFX(sellSfx);

            GameEvents.Potion.OnPotionSold?.Invoke(sellValue);

            Debug.Log($"Sold potion for {sellValue}");

            sellData.amount -= 1;

            if (sellData.amount < 1)
            {
                potionsToSell.Remove(sellData);
            }

            ClearShop();

            ShowPotions();
        }
    }
}
