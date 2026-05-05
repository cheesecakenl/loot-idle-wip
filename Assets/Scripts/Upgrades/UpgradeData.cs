using UnityEngine;

[CreateAssetMenu(menuName = "LootIdle/Create Upgrade Data...")]
public class UpgradeData : ScriptableObject
{
    [SerializeField, Tooltip("This upgrade effects the chosen stat")]
    public StatType statType;

    public string label;
    public string description;
    public CurrencyType costsCurrencyType;
    public double[] costs;
    public ModifierType modifierType;
    public double[] values;
    public int maxLevel = 0;
    public int unlockAtParentLevel = 1;
    public bool isInfinite = false; // will use the first cost and value for calculations
    public float costIncreaseMultiplier = 1f;
    public UpgradeData parent;
}