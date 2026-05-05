using UnityEngine;
using UnityEngine.SceneManagement;

public class UpgradeTreeManager : MonoBehaviour
{
    public static UpgradeTreeManager instance = null;

    void OnEnable()
    {
        GameEvents.UI.OnUpgradeClicked += HandleOnUpgradeClicked;
    }

    void OnDisable()
    {
        GameEvents.UI.OnUpgradeClicked -= HandleOnUpgradeClicked;
    }

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
    }

    void Start()
    {
        MouseCursor.instance.ShowCursor(MouseCursor.MouseCursorType.HAND);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene("Gameplay");
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("UpgradeTree");
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void HandleOnUpgradeClicked(string label)
    {
        if (label == null || label.Length < 1) return;

        UpgradeInstance upgradeInstance = UpgradesManager.instance.GetUpgradeInstance(label);

        if (upgradeInstance == null) return;

        // Buy upgrade
        bool buyUpgrade = CurrencyManager.instance.BuyUpgrade(upgradeInstance);
        if (!buyUpgrade)
        {
            return;
        }

        upgradeInstance.LevelUp();

        UpgradeTreeUIManager.instance.CheckUnlocks();
    }
}