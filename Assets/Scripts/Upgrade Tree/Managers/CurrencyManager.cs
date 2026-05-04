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
        GameEvents.Player.OnClickedEnemy += HandleOnClickedEnemy;
    }

    void OnDisable()
    {
        GameEvents.Player.OnDataReset -= HandleOnPlayerDataReset;
        GameEvents.Player.OnClickedEnemy -= HandleOnClickedEnemy;
    }

    void Start()
    {
        UpdateCurrencyUI();
    }

    private void UpdateCurrencyUI()
    {
        if (SaveManager.instance.PlayerData == null)
        {
            return;
        }

        Currency gold = SaveManager.instance.PlayerData.GetCurrency(CurrencyType.GOLD);

        if (gold != null)
        {
            int goldRounded = Mathf.RoundToInt(gold.amount);
            goldAmountTextField.text = goldRounded.ToString();
        }
    }

    public bool BuyUpgrade(OwnedUpgrade ownedUpgrade)
    {
        if (ownedUpgrade == null)
        {
            return false;
        }

        if (!ownedUpgrade.IsInfinite && ownedUpgrade.currentLevel == ownedUpgrade.MaxLevel)
        {
            return false;
        }

        CurrencyType currencyType = ownedUpgrade.CostsCurrencyType;
        Currency ownedCurrency = SaveManager.instance.PlayerData.GetCurrency(currencyType);

        if (ownedCurrency == null)
        {
            return false;
        }

        float costs = ownedUpgrade.GetCost();

        if (ownedCurrency.amount < costs)
        {
            return false;
        }

        ownedCurrency.amount -= costs;

        UpdateCurrencyUI();

        return true;
    }

    public bool CanBuyUpgrade(OwnedUpgrade ownedUpgrade)
    {
        if (ownedUpgrade == null) return false;

        CurrencyType currencyType = ownedUpgrade.CostsCurrencyType;
        Currency ownedCurrency = SaveManager.instance.PlayerData.GetCurrency(currencyType);

        if (ownedCurrency == null)
        {
            return false;
        }

        float costs = ownedUpgrade.GetCost();

        if (ownedCurrency.amount >= costs)
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