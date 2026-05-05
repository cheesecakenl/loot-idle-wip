using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeNode : MonoBehaviour, IPointerExitHandler
{
    public UpgradeData data;

    void Start()
    {
        if (data == null)
        {
            throw new Exception("No data for " + gameObject.name);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UpgradeTreeUIManager.instance.HideTooltip();
    }

    public void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameEvents.UI.OnUpgradeClicked?.Invoke(data.label);
        }

        UpgradeInstance upgradeInstance = UpgradesManager.instance.GetUpgradeInstance(data.label);

        if (upgradeInstance == null) return;

        UpgradeTreeUIManager.instance.ShowTooltip(upgradeInstance, transform.position);
    }
}