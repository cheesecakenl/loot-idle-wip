using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UpgradeTreeUIManager : MonoBehaviour
{
    public static UpgradeTreeUIManager instance = null;

    public GameObject tooltip;
    public float tooltipYOffset = 2.5f;

    public TMP_Text tooltipLabel;
    public TMP_Text tooltipLevel;
    public TMP_Text tooltipDescription;
    public TMP_Text tooltipChanges;
    public Image tooltipIcon;
    public TMP_Text tooltipCurrencyAmount;

    public Sprite bgSpriteCanBuy;
    public Sprite bgSpriteCannotBuy;
    public Sprite bgSpriteIsMaxLevel;

    private bool isShowingTooltip = false;

    private UpgradeNode[] upgradeNodes;

    void OnEnable()
    {
        GameEvents.UI.OnUpgradeClicked += HandleOnUpgradeClicked;
        GameEvents.Player.OnDataReset += HandleOnPlayerDataReset;
        GameEvents.UI.OnTreePanningOrZooming += HandleOnTreePanningOrZooming;
    }

    void OnDisable()
    {
        GameEvents.UI.OnUpgradeClicked -= HandleOnUpgradeClicked;
        GameEvents.Player.OnDataReset -= HandleOnPlayerDataReset;
        GameEvents.UI.OnTreePanningOrZooming -= HandleOnTreePanningOrZooming;
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

    private void Start()
    {
        MouseCursor.instance.ShowCursor(MouseCursor.MouseCursorType.HAND);

        upgradeNodes = GetComponentsInChildren<UpgradeNode>();

        HideTooltip();

        CheckUnlocks();
    }

    private void Update()
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

    public void ShowTooltip(UpgradeInstance upgradeInstance, Vector3 nodePosition)
    {
        string maxLevel = "" + upgradeInstance.data.maxLevel;
        if (upgradeInstance.data.isInfinite)
        {
            maxLevel = "-";
        }

        tooltipLabel.text = upgradeInstance.data.label;
        tooltipLevel.text = upgradeInstance.level + " / " + maxLevel;
        tooltipDescription.text = GetDescription(upgradeInstance);
        tooltipChanges.text = upgradeInstance.IsMaxLevel ? "MAX" : upgradeInstance.GetValue().ToString() + " -> " + upgradeInstance.GetNextValue().ToString();

        if (!upgradeInstance.IsMaxLevel)
        {
            ShowCurrency(upgradeInstance);
        }
        else
        {
            HideCurrency();
        }

        tooltip.SetActive(true);

        // animate once
        if (!isShowingTooltip)
        {
            tooltip.transform.DOKill();
            tooltip.transform.localScale = Vector3.one;
            tooltip.transform.DOShakeScale(0.3f, strength: 0.2f);
        }

        tooltip.transform.position = new Vector3(nodePosition.x, nodePosition.y + tooltipYOffset, nodePosition.z); ;

        isShowingTooltip = true;
    }

    private void ShowCurrency(UpgradeInstance upgradeInstance)
    {
        tooltipIcon.enabled = true;
        tooltipCurrencyAmount.text = upgradeInstance.GetCost().ToString();
    }

    private void HideCurrency()
    {
        tooltipIcon.enabled = false;
        tooltipCurrencyAmount.text = "";
    }

    private string GetDescription(UpgradeInstance upgradeInstance)
    {
        string descr = upgradeInstance.data.description;

        if (upgradeInstance.data.modifierType == ModifierType.FLAT)
        {
            descr += "<color=#00FF00> +" + upgradeInstance.GetValue() + "</color>";
        }
        if (upgradeInstance.data.modifierType == ModifierType.PERCENTAGE)
        {
            descr += "<color=#00FF00> +" + upgradeInstance.GetValue() + "%</color>";
        }
        if (upgradeInstance.data.modifierType == ModifierType.MULTIPLIER)
        {
            descr += "<color=#00FF00> x" + upgradeInstance.GetValue() + "</color>";
        }

        return descr;
    }

    public void HideTooltip()
    {
        tooltip.SetActive(false);

        isShowingTooltip = false;
    }

    public void CheckUnlocks()
    {
        foreach (UpgradeNode node in upgradeNodes)
        {
            UpgradeInstance ownedUpgrade = UpgradesManager.instance.GetUpgradeInstance(node.data.label);
            UpgradeInstance parentUpgrade = UpgradesManager.instance.GetUpgradeInstance(node.data.parent == null ? "" : node.data.parent.label);

            bool canBuyUpgrade = UpgradesManager.instance.CanBuyUpgrade(ownedUpgrade);

            Image bgImage = node.transform.Find("BG")?.GetComponent<Image>();
            if (bgImage != null)
            {
                if (canBuyUpgrade)
                {
                    bgImage.sprite = bgSpriteCanBuy;
                }
                else
                {
                    bgImage.sprite = bgSpriteCannotBuy;
                }

                if (ownedUpgrade.IsMaxLevel)
                {
                    bgImage.sprite = bgSpriteIsMaxLevel;
                }
            }

            if (node.data.parent != null && parentUpgrade.level < node.data.unlockAtParentLevel)
            {
                node.gameObject.SetActive(false);
            }
            else
            {
                node.gameObject.SetActive(true);
            }
        }
    }

    private void HandleOnTreePanningOrZooming()
    {
        HideTooltip();
    }

    private void HandleOnPlayerDataReset()
    {
        CheckUnlocks();
    }

    private void HandleOnUpgradeClicked(string label)
    {
        if (label == null || label.Length < 1) return;

        UpgradeInstance upgradeInstance = UpgradesManager.instance.GetUpgradeInstance(label);

        if (upgradeInstance == null) return;

        // Buy upgrade
        bool buyUpgrade = UpgradesManager.instance.BuyUpgrade(upgradeInstance);
        if (!buyUpgrade)
        {
            return;
        }

        upgradeInstance.LevelUp();

        UpgradeTreeUIManager.instance.CheckUnlocks();

        PlayerManager.instance.Save();
    }
}