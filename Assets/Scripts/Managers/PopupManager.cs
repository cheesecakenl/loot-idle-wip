using TMPro;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    public static PopupManager instance = null;

    [SerializeField] private GameObject popupAmountPrefab;

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

    public void ShowAmountPopup(double amount, Color color, Vector3 position)
    {
        position.z = 0f;

        GameObject clone = Instantiate(popupAmountPrefab, position, Quaternion.identity);
        TMP_Text amountText = clone.GetComponentInChildren<TMP_Text>();

        string formattedAmount = MoneyHelper.FormatMoney(amount);

        amountText.text = "+" + formattedAmount;
        amountText.color = color;
    }
}
