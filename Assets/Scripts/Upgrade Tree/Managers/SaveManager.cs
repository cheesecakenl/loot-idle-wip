using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance = null;

    private string savePath;

    private PlayerData playerData;
    public PlayerData PlayerData => playerData;

    private List<UpgradeData> allUpgrades;
    public List<UpgradeData> AllUpgrades => allUpgrades;

    private List<Currency> allCurrencies;
    public List<Currency> AllCurrencies => allCurrencies;

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

        savePath = Application.persistentDataPath + "/upgrade-tree-save.json";

        allUpgrades = new List<UpgradeData>();
        UpgradeData[] upgrades = Resources.LoadAll<UpgradeData>("Data/Upgrades/");
        foreach (UpgradeData upgrade in upgrades)
        {
            allUpgrades.Add(upgrade);
        }

        allCurrencies = new List<Currency>();

        foreach (CurrencyType type in Enum.GetValues(typeof(CurrencyType)))
        {
            Currency currency = new Currency();
            currency.type = type;
            currency.label = type.ToString();

            allCurrencies.Add(currency);
        }

        playerData = Load();

        if (playerData == null)
        {
            playerData = CreatePlayerData();

            Save();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetPlayerData();
        }
    }

    private void ResetPlayerData()
    {
        foreach (Currency currency in playerData.ownedCurrencies)
        {
            if (currency.type == CurrencyType.GOLD)
            {
                currency.amount = 5000f;
            }
            else
            {
                currency.amount = 0f;
            }
        }

        foreach (OwnedUpgrade upgrade in playerData.ownedUpgrades)
        {
            upgrade.currentLevel = 0;
        }

        Save();

        GameEvents.Player.OnDataReset?.Invoke();
    }

    private void AddUpgradeData(PlayerData data)
    {
        foreach (OwnedUpgrade ownedUpgrade in data.ownedUpgrades)
        {
            foreach (UpgradeData upgradeData in allUpgrades)
            {
                if (upgradeData.ID == ownedUpgrade.ID)
                {
                    ownedUpgrade.Init(upgradeData);
                }
            }
        }
    }

    public void Save()
    {
        string json = JsonUtility.ToJson(playerData, true);
        File.WriteAllText(savePath, json);

        Debug.Log("Saved to: " + savePath);
    }

    private PlayerData CreatePlayerData()
    {
        PlayerData playerData = new PlayerData();
        playerData.ownedCurrencies = allCurrencies;

        List<OwnedUpgrade> ownedUpgrades = new List<OwnedUpgrade>();
        foreach (UpgradeData upgrade in allUpgrades)
        {
            if (upgrade != null)
            {
                OwnedUpgrade ownedUpgrade = new OwnedUpgrade();
                ownedUpgrade.ID = upgrade.ID;
                ownedUpgrade.label = upgrade.label;
                ownedUpgrade.currentLevel = 0;

                ownedUpgrades.Add(ownedUpgrade);
            }
        }
        playerData.ownedUpgrades = ownedUpgrades;

        AddUpgradeData(playerData);

        return playerData;
    }

    private PlayerData Load()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);

            PlayerData data = JsonUtility.FromJson<PlayerData>(json);

            if (data == null) return null;

            AddUpgradeData(data);

            Debug.Log("Loaded from: " + savePath);

            return data;
        }

        return null;
    }
}