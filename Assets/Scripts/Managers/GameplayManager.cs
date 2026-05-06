using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager instance = null;

    public static event Action<double> OnUpdateMoneyUI;

    [SerializeField] private Texture2D mouseTexture;
    [SerializeField] private GameObject chestPrefab;
    [SerializeField] private GameObject piggyBankPrefab;

    private Vector2 clickHotspot;

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

        clickHotspot = new Vector2(0f, 0f);
    }

    void OnEnable()
    {
        Coin.OnCoinPickup += HandleOnCoinPickup;
    }

    void OnDisable()
    {
        Coin.OnCoinPickup -= HandleOnCoinPickup;
    }

    private void Start()
    {
        Cursor.SetCursor(mouseTexture, clickHotspot, CursorMode.ForceSoftware);
        Cursor.visible = true;

        AudioManager.instance.ChangeBgmVolume(0.1f);
        AudioManager.instance.PlayMusic(AudioType.BGM, "gameplay");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Gameplay");
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            SceneManager.LoadScene("UpgradeTree");
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (Input.GetMouseButtonDown(1))
        {
            SpawnChest();
        }
    }

    void SpawnChest()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        GameObject prefab = chestPrefab;

        bool piggyBankUnlocked = UpgradesManager.instance.IsUpgradeUnlocked(StatType.PIGGY_BANK_UNLOCK);
        if (piggyBankUnlocked)
        {
            int rand = UnityEngine.Random.Range(0, 100);
            if (rand < 50)
            {
                prefab = piggyBankPrefab;
            }
        }

        GameObject clone = Instantiate(prefab, mousePos, Quaternion.identity);
    }

    private void HandleOnCoinPickup(double amount)
    {
        PlayerManager.instance.Gain(amount);

        OnUpdateMoneyUI?.Invoke(PlayerManager.instance.money);
    }
}