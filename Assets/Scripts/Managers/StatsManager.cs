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

    // At least one FLAT modifier needed for formula.

    // FLAT       = creates/adds value
    // PERCENTAGE = scales existing value
    // MULTIPLIER = scales final value

    // Pass base value 0 when calculating for discounts

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

        return (baseValue + flatSum) * (1 + percentSum / 100) * multiplierProduct;
    }
}