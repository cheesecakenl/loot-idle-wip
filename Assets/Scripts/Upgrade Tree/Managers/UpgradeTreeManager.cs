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

    private void HandleOnUpgradeClicked(string ID)
    {
        if (SaveManager.instance.PlayerData == null)
        {
            return;
        }

        OwnedUpgrade ownedUpgrade = SaveManager.instance.PlayerData.GetOwnedUpgrade(ID);
        if (ownedUpgrade == null)
        {
            return;
        }

        // Buy upgrade
        bool buyUpgrade = CurrencyManager.instance.BuyUpgrade(ownedUpgrade);
        if (!buyUpgrade)
        {
            return;
        }

        ownedUpgrade.LevelUp();

        SaveManager.instance.Save();

        UpgradeTreeUIManager.instance.CheckUnlocks();
    }
}