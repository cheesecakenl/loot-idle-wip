using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "LootIdle/Create Upgrade Data...")]
public class UpgradeData : ScriptableObject
{
    [SerializeField, HideInInspector]
    private string id;

    public string ID => id; // read-only property for runtime

    [SerializeField, Tooltip("This upgrade effects the chosen stat")]
    public StatType statType;

    public string label;
    public string description;
    public CurrencyType costsCurrencyType;
    public int[] costs;
    public ModifierType modifierType;
    public float[] values;
    public int maxLevel = 10;
    public int unlockAtParentLevel = 1;
    public bool isInfinite = false; // will use the first cost and value for calculations
    public float costIncreaseMultiplier = 1f;
    public UpgradeData parent;

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (string.IsNullOrEmpty(id))
        {
            id = System.Guid.NewGuid().ToString();
            EditorUtility.SetDirty(this);
        }
    }
#endif

#if UNITY_EDITOR
    [ContextMenu("Regenerate ID")]
    private void RegenerateId()
    {
        id = System.Guid.NewGuid().ToString();
        EditorUtility.SetDirty(this);
    }
#endif
}