using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "LootIdle/Create Upgrades Database...")]
public class UpgradesDatabaseSO : ScriptableObject
{
    [SerializeField]
    public List<UpgradeData> allUpgrades;
}