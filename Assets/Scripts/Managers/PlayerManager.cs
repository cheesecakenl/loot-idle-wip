using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance = null;

    public double money;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        //DontDestroyOnLoad(gameObject);

        Init();
    }

    private void Init()
    {
        money = 50000;

        SaveData saveData = SaveManager.Load();

        if (saveData == null) return;

        money = saveData.money;
    }

    public string GetFormattedMoney()
    {
        string[] suffixes = { "", "K", "M", "B", "T", "Qa", "Qi", "Sx", "Sp", "Oc", "No", "Dc" };

        double tempMoney = money;

        if (tempMoney < 1000)
            return tempMoney.ToString("0");

        int suffixIndex = 0;

        while (tempMoney >= 1000 && suffixIndex < suffixes.Length - 1)
        {
            tempMoney /= 1000;
            suffixIndex++;
        }

        // Fallback to scientific notation if we ran out of suffixes
        if (suffixIndex == suffixes.Length - 1 && tempMoney >= 1000)
        {
            return money.ToString("0.000e+0");
        }

        return tempMoney.ToString("0.##") + suffixes[suffixIndex];
    }

    public void Pay(double amount)
    {
        money -= amount;
    }

    public void Gain(double amount)
    {
        money += amount;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Save();
        }
    }

    public void Save()
    {
        SaveData saveData = new();
        saveData.money = money;
        saveData.upgrades = UpgradesManager.instance.ConvertToUpgradeSaveData();

        SaveManager.Save(saveData);
    }
}
