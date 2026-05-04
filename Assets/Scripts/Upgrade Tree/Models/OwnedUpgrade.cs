using System;
using UnityEngine;

[Serializable]
public class OwnedUpgrade
{
    public string ID;
    public string label;
    public int currentLevel;

    private UpgradeData data;

    public void Init(UpgradeData scriptableObjectData)
    {
        data = scriptableObjectData;
    }

    public int UnlockAtParentLevel => data.unlockAtParentLevel;

    public UpgradeData Parent => data.parent;

    public string Description => data.description;

    public int MaxLevel => data.maxLevel;

    public bool IsInfinite => data.isInfinite;

    public StatType StatType => data.statType;

    public ModifierType ModifierType => data.modifierType;

    public CurrencyType CostsCurrencyType => data.costsCurrencyType;

    public bool IsMaxLevel => data.isInfinite ? false : currentLevel >= data.maxLevel;

    public int GetCost()
    {
        if (data.isInfinite)
        {
            float cost = data.costs[0] * Mathf.Pow(data.costIncreaseMultiplier, currentLevel);
            int rounded = Mathf.RoundToInt(cost);

            return rounded;
        }

        return data.costs[IsMaxLevel ? currentLevel - 1 : currentLevel];
    }

    public float GetValue()
    {
        if (data.isInfinite)
        {
            return currentLevel == 0 ? 0 : data.values[0] * currentLevel;
        }

        if (currentLevel == 0)
        {
            return 0;
        }

        return data.values[currentLevel - 1];
    }

    public float GetNextValue()
    {
        if (data.isInfinite)
        {
            return currentLevel == 0 ? data.values[0] * 1 : data.values[0] * (currentLevel + 1);
        }

        if (currentLevel == 0)
        {
            return data.values[0];
        }

        if (IsMaxLevel)
        {
            return data.values[currentLevel - 1];
        }

        return data.values[currentLevel];

    }

    public void ResetUpgrade()
    {
        currentLevel = 0;
    }

    public void LevelUp()
    {
        if (!data.isInfinite && currentLevel >= data.maxLevel)
            return;

        currentLevel++;
    }

    public void LevelDown()
    {
        if (currentLevel < 1)
            return;

        currentLevel--;
    }
}