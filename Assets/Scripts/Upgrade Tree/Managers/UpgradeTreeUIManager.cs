using DG.Tweening;
using TMPro;
using UnityEngine;
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
        GameEvents.Player.OnDataReset += HandleOnPlayerDataReset;
        GameEvents.UI.OnTreePanningOrZooming += HandleOnTreePanningOrZooming;
    }

    void OnDisable()
    {
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

        upgradeNodes = GetComponentsInChildren<UpgradeNode>();
    }

    private void Start()
    {
        HideTooltip();

        CheckUnlocks();
    }

    public void ShowTooltip(OwnedUpgrade ownedUpgrade, Vector3 nodePosition)
    {
        string maxLevel = "" + ownedUpgrade.MaxLevel;
        if (ownedUpgrade.IsInfinite)
        {
            maxLevel = "-";
        }

        tooltipLabel.text = ownedUpgrade.label;
        tooltipLevel.text = ownedUpgrade.currentLevel + " / " + maxLevel;
        tooltipDescription.text = GetDescription(ownedUpgrade);
        tooltipChanges.text = ownedUpgrade.IsMaxLevel ? "MAX" : ownedUpgrade.GetValue().ToString() + " -> " + ownedUpgrade.GetNextValue().ToString();

        if (!ownedUpgrade.IsMaxLevel)
        {
            ShowCurrency(ownedUpgrade);
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

    private void ShowCurrency(OwnedUpgrade ownedUpgrade)
    {
        Sprite sprite = CurrencyManager.instance.GetSpriteForCurrency(ownedUpgrade.CostsCurrencyType);
        if (sprite != null)
        {
            tooltipIcon.sprite = sprite;
            tooltipIcon.enabled = true;
        }
        tooltipCurrencyAmount.text = ownedUpgrade.GetCost().ToString();
    }

    private void HideCurrency()
    {
        tooltipIcon.enabled = false;
        tooltipCurrencyAmount.text = "";
    }

    private string GetDescription(OwnedUpgrade ownedUpgrade)
    {
        string descr = ownedUpgrade.Description;

        if (ownedUpgrade.ModifierType == ModifierType.FLAT)
        {
            descr += "<color=#00FF00> +" + ownedUpgrade.GetValue() + "</color>";
        }
        if (ownedUpgrade.ModifierType == ModifierType.PERCENTAGE)
        {
            descr += "<color=#00FF00> +" + ownedUpgrade.GetValue() + "%</color>";
        }
        if (ownedUpgrade.ModifierType == ModifierType.MULTIPLIER)
        {
            descr += "<color=#00FF00> x" + ownedUpgrade.GetValue() + "</color>";
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
            OwnedUpgrade ownedUpgrade = SaveManager.instance.PlayerData.GetOwnedUpgrade(node.data.ID);
            OwnedUpgrade parentUpgrade = SaveManager.instance.PlayerData.GetOwnedUpgrade(node.data.parent == null ? "" : node.data.parent.ID);

            bool canBuyUpgrade = CurrencyManager.instance.CanBuyUpgrade(ownedUpgrade);

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

            if (node.data.parent != null && parentUpgrade.currentLevel < node.data.unlockAtParentLevel)
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
}