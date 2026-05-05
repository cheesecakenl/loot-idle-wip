using System.Collections.Generic;

public class Stat
{
    public StatType type;
    public string label;
    public double baseValue;

    private double flatSum;
    private double percentSum;
    private double multiplierProduct = 1;

    List<UpgradeInstance> modifiers = new List<UpgradeInstance>();

    public void AddModifier(UpgradeInstance mod)
    {
        bool modifierExists = ModifierExists(mod);
        if (modifierExists)
        {
            modifiers.Remove(mod);
        }

        modifiers.Add(mod);
    }

    private bool ModifierExists(UpgradeInstance mod)
    {
        foreach (UpgradeInstance modifier in modifiers)
        {
            if (modifier.data.label == mod.data.label)
            {
                return true;
            }
        }

        return false;
    }

    private void Calculate()
    {
        foreach (UpgradeInstance mod in modifiers)
        {
            if (mod.data.modifierType == ModifierType.FLAT)
                flatSum += mod.GetValue();

            else if (mod.data.modifierType == ModifierType.PERCENTAGE)
                percentSum += mod.GetValue() / 100;

            else if (mod.data.modifierType == ModifierType.MULTIPLIER)
                multiplierProduct *= mod.GetValue();
        }
    }

    public double GetValue()
    {
        flatSum = 0f;
        percentSum = 0f;
        multiplierProduct = 1f;

        Calculate();

        return (baseValue + flatSum) * (1f + percentSum) * multiplierProduct;
    }
}