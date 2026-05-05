using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "LootIdle/Create Currency Sprite Database...")]
public class CurrencySpriteDatabase : ScriptableObject
{
    [SerializeField]
    public List<CurrencySprite> entries;

    [System.Serializable]
    public class CurrencySprite
    {
        public CurrencyType currencyType;
        public Sprite sprite;
    }

    public CurrencySprite GetEntry(CurrencyType currencyType)
    {
        foreach (CurrencySprite entry in entries)
        {
            if (entry.currencyType == currencyType)
            {
                return entry;
            }
        }

        return null;
    }
}