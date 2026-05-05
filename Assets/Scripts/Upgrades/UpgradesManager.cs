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

        foreach(UpgradeData data in upgradesDatabase.allUpgrades)
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
}
