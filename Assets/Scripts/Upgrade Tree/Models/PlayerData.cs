using System;
using System.Collections.Generic;

[Serializable]
public class PlayerData
{
    public List<Currency> ownedCurrencies;
    public List<OwnedUpgrade> ownedUpgrades;

    public OwnedUpgrade GetOwnedUpgrade(string ID)
    {
        foreach (OwnedUpgrade ownedUpgrade in ownedUpgrades)
        {
            if (ownedUpgrade.ID == ID)
            {
                return ownedUpgrade;
            }
        }

        return null;
    }

    public Currency GetCurrency(CurrencyType type)
    {
        foreach (Currency currency in ownedCurrencies)
        {
            if (currency.type == type)
            {
                return currency;
            }
        }

        return null;
    }
}