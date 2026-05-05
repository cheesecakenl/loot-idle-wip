using System.Collections.Generic;
using UnityEngine;

public class UpgradesManager : MonoBehaviour
{
    public static UpgradesManager instance = null;

    [SerializeField] private UpgradesDatabaseSO upgradesDatabase;

    public List<UpgradeInstance> upgradeInstances;

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

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        upgradeInstances = ConvertToUpgradeInstances();
    }

    public List<UpgradeSaveData> ConvertToUpgradeSaveData()
    {
        List<UpgradeSaveData> saveUpgrades = new();

        foreach (UpgradeInstance upgrade in upgradeInstances)
        {
            saveUpgrades.Add(upgrade.ToSaveData());
        }

        return saveUpgrades;
    }

    public List<UpgradeInstance> ConvertToUpgradeInstances()
    {
        SaveData saveData = SaveManager.instance.Load();
        if (saveData == null) return new(); 

        List<UpgradeInstance> loadedUpgrades = new();
        foreach (UpgradeSaveData upgradeSaveData in saveData.upgrades)
        {
            UpgradeData upgradeData = upgradesDatabase.GetByLabel(upgradeSaveData.label);
            if (upgradeData == null)
                continue;

            loadedUpgrades.Add(new UpgradeInstance(upgradeData, upgradeSaveData.level));
        }

        return loadedUpgrades;
    }

    public UpgradeInstance GetUpgradeInstance(string label)
    {
        return upgradeInstances.Find(u => u.data.label == label);
    }
}
