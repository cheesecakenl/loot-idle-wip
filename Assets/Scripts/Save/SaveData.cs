using System;
using System.Collections.Generic;

[Serializable]
public class SaveData
{
    public double money = 0;
    public List<UpgradeSaveData> upgrades = new();
}
