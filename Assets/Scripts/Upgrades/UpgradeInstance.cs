using UnityEngine;

public class UpgradeInstance
{
    public UpgradeData data;
    public int level;

    public UpgradeInstance(UpgradeData data, int level)
    {
        this.data = data;
        this.level = level;
    }

    public UpgradeSaveData ToSaveData()
    {
        return new UpgradeSaveData
        {
            label = data.label,
            level = level
        };
    }

    public bool IsMaxLevel => data.isInfinite ? false : level >= data.maxLevel;

    public double GetCost()
    {
        if (data.isInfinite)
        {
            return data.costs[0] * Mathf.Pow(data.costIncreaseMultiplier, level);
        }

        return data.costs[IsMaxLevel ? level - 1 : level];
    }

    public double GetValue()
    {
        if (data.isInfinite)
        {
            return level == 0 ? 0 : data.values[0] * level;
        }

        if (level == 0)
        {
            return 0;
        }

        return data.values[level - 1];
    }

    public double GetNextValue()
    {
        if (data.isInfinite)
        {
            return level == 0 ? data.values[0] * 1 : data.values[0] * (level + 1);
        }

        if (level == 0)
        {
            return data.values[0];
        }

        if (IsMaxLevel)
        {
            return data.values[level - 1];
        }

        return data.values[level];
    }

    public void ResetLevel()
    {
        level = 0;
    }

    public void LevelUp()
    {
        if (!data.isInfinite && level >= data.maxLevel)
            return;

        level++;
    }

    public void LevelDown()
    {
        if (level < 1)
            return;

        level--;
    }
}