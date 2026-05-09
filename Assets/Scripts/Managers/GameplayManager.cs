using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager instance = null;

    [SerializeField] private Texture2D mouseTexture;
    [SerializeField] private GameObject chestPrefab;
    [SerializeField] private GameObject piggyBankPrefab;
    [SerializeField] private GameObject mushroomPrefab;

    private Vector2 clickHotspot;

    [SerializeField] public bool playBGM = false;

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
        GameEvents.Potion.OnPotionSold += HandleOnPotionSold;
    }

    void OnDisable()
    {
        GameEvents.Potion.OnPotionSold -= HandleOnPotionSold;
    }

    private void Start()
    {
        Cursor.SetCursor(mouseTexture, clickHotspot, CursorMode.ForceSoftware);
        Cursor.visible = true;

        if (playBGM)
        {
            AudioManager.instance.ChangeBgmVolume(0.1f);
            AudioManager.instance.PlayMusic(AudioType.BGM, "gameplay");
        }
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
            //SpawnChest();

            SpawnMushroom();
        }
    }

    void SpawnMushroom()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        GameObject clone = Instantiate(mushroomPrefab, mousePos, Quaternion.identity);
    }

    private void HandleOnPotionSold(double amount)
    {
        PlayerManager.instance.Gain(amount);
    }
}