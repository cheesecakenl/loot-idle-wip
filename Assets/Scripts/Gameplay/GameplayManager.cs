using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager instance = null;

    public static event Action<int> OnUpdateMoneyUI;

    [SerializeField] private Texture2D mouseTexture;
    [SerializeField] private GameObject chestPrefab;

    private int totalMoney;

    public int Money => totalMoney;

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
            SceneManager.LoadScene("LootIdle");
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

        GameObject clone = Instantiate(chestPrefab, mousePos, Quaternion.identity);
    }

    private void HandleOnCoinPickup(int amount)
    {
        totalMoney += amount;

        OnUpdateMoneyUI?.Invoke(totalMoney);
    }
}