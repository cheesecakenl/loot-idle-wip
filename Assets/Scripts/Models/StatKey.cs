public readonly struct StatKey
{
    public readonly GameData Target;
    public readonly StatType StatType;

    public StatKey(GameData target, StatType statType)
    {
        Target = target;
        StatType = statType;
    }
}