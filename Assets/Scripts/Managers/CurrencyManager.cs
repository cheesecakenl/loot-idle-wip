using TMPro;
using UnityEngine;
using static CurrencySpriteDatabase;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager instance = null;

    public TMP_Text goldAmountTextField;

    public CurrencySpriteDatabase currencySpriteDatabase;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        //DontDestroyOnLoad(gameObject);
    }

    void OnEnable()
    {
        GameEvents.Player.OnDataReset += HandleOnPlayerDataReset;
    }

    void OnDisable()
    {
        GameEvents.Player.OnDataReset -= HandleOnPlayerDataReset;
    }

    void Start()
    {
        UpdateCurrencyUI();
    }

    private void UpdateCurrencyUI()
    {
        string formattedMoney = PlayerManager.instance.GetFormattedMoney();
        goldAmountTextField.text = formattedMoney;
    }

    public bool BuyUpgrade(UpgradeInstance upgradeInstance)
    {
        if (upgradeInstance == null)
        {
            return false;
        }

        if (!upgradeInstance.data.isInfinite && upgradeInstance.level == upgradeInstance.data.maxLevel)
        {
            return false;
        }

        double money = PlayerManager.instance.money;
        if (money <= 0)
        {
            return false;
        }

        double costs = upgradeInstance.GetCost();
        if (money < costs)
        {
            return false;
        }

        PlayerManager.instance.Pay(costs);

        UpdateCurrencyUI();

        return true;
    }

    public bool CanBuyUpgrade(UpgradeInstance upgradeInstance)
    {
        if (upgradeInstance == null) return false;

        double money = PlayerManager.instance.money;
        if (money <= 0)
        {
            return false;
        }

        double costs = upgradeInstance.GetCost();
        if (money >= costs)
        {
            return true;
        }

        return false;
    }

    public Sprite GetSpriteForCurrency(CurrencyType type)
    {
        if (currencySpriteDatabase == null)
        {
            return null;
        }

        CurrencySprite currencySprite = currencySpriteDatabase.GetEntry(type);

        if (currencySprite != null)
        {
            return currencySprite.sprite;
        }

        return null;
    }

    private void HandleOnPlayerDataReset()
    {
        UpdateCurrencyUI();
    }

    private void HandleOnClickedEnemy()
    {
        UpdateCurrencyUI();
    }
}