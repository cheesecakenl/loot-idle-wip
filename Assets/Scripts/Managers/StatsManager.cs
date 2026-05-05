using System;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    public static StatsManager instance = null;

    private List<Stat> allStats;

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

        allStats = CreateAllStats();
    }

    private List<Stat> CreateAllStats()
    {
        List<Stat> stats = new List<Stat>();

        foreach (StatType type in Enum.GetValues(typeof(StatType)))
        {
            Stat stat = new Stat();
            stat.type = type;
            stat.label = type.ToString();

            stats.Add(stat);
        }

        return stats;
    }

    void Start()
    {
        AddModifiersToStats();
    }

    private void AddModifiersToStats()
    {
        if (UpgradesManager.instance == null) return;

        foreach (Stat stat in allStats)
        {
            foreach (UpgradeInstance upgradeInstance in UpgradesManager.instance.allUpgrades)
            {
                if (upgradeInstance.level < 1)
                {
                    continue;
                }

                if (stat.type == upgradeInstance.data.statType)
                {
                    stat.AddModifier(upgradeInstance);
                }
            }
        }

        foreach (Stat stat in allStats)
        {
            Debug.Log("STAT " + stat.label + ": " + stat.GetValue());
        }
    }

    public Stat GetStat(StatType type)
    {
        foreach (Stat stat in allStats)
        {
            if (stat.type == type)
            {
                return stat;
            }
        }

        return null;
    }
}