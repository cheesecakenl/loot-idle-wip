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
            GameEvents.UI.OnUpgradeClicked?.Invoke(data.ID);
        }

        if (SaveManager.instance.PlayerData == null)
        {
            return;
        }

        OwnedUpgrade ownedUpgrade = SaveManager.instance.PlayerData.GetOwnedUpgrade(data.ID);
        UpgradeTreeUIManager.instance.ShowTooltip(ownedUpgrade, transform.position);
    }
}