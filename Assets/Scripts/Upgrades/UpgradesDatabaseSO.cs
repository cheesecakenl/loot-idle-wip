using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "LootIdle/Create Upgrades Database...")]
public class UpgradesDatabaseSO : ScriptableObject
{
    [SerializeField]
    private List<UpgradeData> allUpgrades;

    public UpgradeData GetByLabel(string label)
    {
        return allUpgrades.Find(u => u.label == label);
    }
}