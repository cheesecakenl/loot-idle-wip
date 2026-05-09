using System.Collections.Generic;
using UnityEngine;

public class UpgradesManager : MonoBehaviour
{
    public static UpgradesManager instance = null;

    [SerializeField] private UpgradesDatabaseSO upgradesDatabase;

    public List<UpgradeInstance> allUpgrades;

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

        LoadAllUpgrades();
    }

    private void LoadAllUpgrades()
    {
        allUpgrades = new();

        SaveData saveData = SaveManager.Load();

        foreach (UpgradeData data in upgradesDatabase.allUpgrades)
        {
            int currentLevel = 0;

            if (saveData != null)
            {
                foreach (UpgradeSaveData upgradeSaveData in saveData.upgrades)
                {
                    if (upgradeSaveData.label == data.label)
                    {
                        currentLevel = upgradeSaveData.level;
                    }
                }
            }

            UpgradeInstance instance = new UpgradeInstance(data, currentLevel);

            allUpgrades.Add(instance);
        }
    }

    public List<UpgradeSaveData> ConvertToUpgradeSaveData()
    {
        List<UpgradeSaveData> saveUpgrades = new();

        foreach (UpgradeInstance upgrade in allUpgrades)
        {
            saveUpgrades.Add(upgrade.ToSaveData());
        }

        return saveUpgrades;
    }

    public UpgradeInstance GetUpgradeInstance(string label)
    {
        return allUpgrades.Find(u => u.data.label == label);
    }

    public bool IsUpgradeUnlocked(StatType statType)
    {
        foreach (UpgradeInstance upgradeInstance in allUpgrades)
        {
            if (upgradeInstance.data.statType == statType && upgradeInstance.level > 0)
            {
                return true;
            }
        }

        return false;
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
}
