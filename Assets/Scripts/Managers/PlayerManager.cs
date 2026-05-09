using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance = null;

    public double money;

    [SerializeField] private float autoSaveTimerInterval = 60f;
    private float autoSaveTimer;

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Save();
        }

        if (autoSaveTimer > autoSaveTimerInterval)
        {
            Save();

            autoSaveTimer = 0f;
        }

        autoSaveTimer += Time.deltaTime;
    }

    private void Init()
    {
        money = 1;

        SaveData saveData = SaveManager.Load();

        if (saveData == null) return;

        money = saveData.money;
    }

    private void Start()
    {
        GameEvents.Money.OnMoneyChanged?.Invoke(money);
    }

    public void Pay(double amount)
    {
        money -= amount;

        GameEvents.Money.OnMoneyChanged?.Invoke(money);
    }

    public void Gain(double amount)
    {
        money += amount;

        GameEvents.Money.OnMoneyChanged?.Invoke(money);
    }

    public void Save()
    {
        SaveData saveData = new();
        saveData.money = money;
        saveData.upgrades = UpgradesManager.instance.ConvertToUpgradeSaveData();

        SaveManager.Save(saveData);
    }
}
