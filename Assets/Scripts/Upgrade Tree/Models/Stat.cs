using System.Collections.Generic;

public class Stat
{
    public StatType type;
    public string label;
    public float baseValue;

    private float flatSum;
    private float percentSum;
    private float multiplierProduct = 1f;

    List<OwnedUpgrade> modifiers = new List<OwnedUpgrade>();

    public void AddModifier(OwnedUpgrade mod)
    {
        bool modifierExists = ModifierExists(mod);
        if (modifierExists)
        {
            modifiers.Remove(mod);
        }

        modifiers.Add(mod);
    }

    private bool ModifierExists(OwnedUpgrade mod)
    {
        foreach (OwnedUpgrade modifier in modifiers)
        {
            if (modifier.ID == mod.ID)
            {
                return true;
            }
        }

        return false;
    }

    private void Calculate()
    {
        foreach (OwnedUpgrade mod in modifiers)
        {
            if (mod.ModifierType == ModifierType.FLAT)
                flatSum += mod.GetValue();

            else if (mod.ModifierType == ModifierType.PERCENTAGE)
                percentSum += mod.GetValue() / 100;

            else if (mod.ModifierType == ModifierType.MULTIPLIER)
                multiplierProduct *= mod.GetValue();
        }
    }

    public float GetValue()
    {
        flatSum = 0f;
        percentSum = 0f;
        multiplierProduct = 1f;

        Calculate();

        return (baseValue + flatSum) * (1f + percentSum) * multiplierProduct;
    }
}