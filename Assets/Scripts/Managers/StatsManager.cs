using System;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    public static StatsManager instance = null;

    [SerializeField] private readonly Dictionary<StatKey, List<UpgradeInstance>> modifiers = new();

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

    private void Start()
    {
        foreach (UpgradeInstance upgradeInstance in UpgradesManager.instance.allUpgrades)
        {
            AddModifier(upgradeInstance);
        }
    }

    public void AddModifier(UpgradeInstance mod)
    {
        StatKey key = new(
            mod.data.target,
            mod.data.statType
        );

        if (!modifiers.TryGetValue(key, out var list))
        {
            list = new List<UpgradeInstance>();
            modifiers.Add(key, list);
        }

        list.RemoveAll(m => m.data == mod.data);

        list.Add(mod);
    }

    public double GetValue(GameData target, StatType type, double baseValue)
    {
        StatKey key = new(target, type);

        if (!modifiers.TryGetValue(key, out var list))
            return baseValue;

        double flatSum = 0;
        double percentSum = 0;
        double multiplierProduct = 1;

        foreach (UpgradeInstance mod in list)
        {
            if (mod.level < 1)
            {
                continue;
            }

            double value = mod.GetValue();

            switch (mod.data.modifierType)
            {
                case ModifierType.FLAT:
                    flatSum += value;
                    break;

                case ModifierType.PERCENTAGE:
                    percentSum += value;
                    break;

                case ModifierType.MULTIPLIER:
                    multiplierProduct *= value;
                    break;
            }
        }

        return (baseValue + flatSum)
            * (1 + percentSum / 100)
            * multiplierProduct;
    }

    private double OldCalc()
    {
        double flatSum = 0f;
        double percentSum = 0f;
        double multiplierProduct = 1f;

        // Change formula based on modifers
        int flatMods = 0;
        int percentMods = 0;
        int multiplierMods = 0;

        foreach (UpgradeInstance mod in UpgradesManager.instance.allUpgrades)
        {
            if (mod.data.modifierType == ModifierType.FLAT)
            {
                flatMods++;
            }
            if (mod.data.modifierType == ModifierType.PERCENTAGE)
            {
                percentMods++;
            }
            if (mod.data.modifierType == ModifierType.MULTIPLIER)
            {
                multiplierMods++;
            }
        }

        if (flatMods > 0)
        {
            return flatSum * (1f + percentSum / 100) * multiplierProduct;
        }
        if (flatMods < 1 && multiplierMods < 1 && percentMods > 0)
        {
            return percentSum;
        }
        if (flatMods < 1 && percentMods < 1 && multiplierMods > 0)
        {
            return multiplierProduct;
        }
        if (flatMods < 1 && percentMods > 0 && multiplierMods > 0)
        {
            return percentSum * multiplierProduct;
        }

        return 0;
    }
}