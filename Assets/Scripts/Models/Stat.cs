using System.Collections.Generic;

public class Stat
{
    public StatType type;
    public string label;

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
                percentSum += mod.GetValue();

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

        // Change formula based on modifers
        int flatMods = 0;
        int percentMods = 0;
        int multiplierMods = 0;
        foreach (UpgradeInstance mod in modifiers)
        {
            if (mod.data.modifierType == ModifierType.FLAT)
            {
                flatMods++;
            }
            if (mod.data.modifierType == ModifierType.PERCENTAGE)
            {
                percentMods++;
            }
            if (mod.data.modifierType == ModifierType.MULTIPLIER)
            {
                multiplierMods++;
            }
        }

        if (flatMods > 0)
        {
            return flatSum * (1f + percentSum / 100 ) * multiplierProduct;
        }
        if (flatMods < 1 && multiplierMods < 1 && percentMods > 0)
        {
            return percentSum;
        }
        if (flatMods < 1 && percentMods < 1 && multiplierMods > 0)
        {
            return multiplierProduct;
        }
        if (flatMods < 1 && percentMods > 0 && multiplierMods > 0)
        {
            return percentSum * multiplierProduct;
        }

        return 0;
    }
}