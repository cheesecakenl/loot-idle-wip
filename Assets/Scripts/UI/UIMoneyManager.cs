using TMPro;
using UnityEngine;

public class UIMoneyManager : MonoBehaviour
{
    public static UIMoneyManager instance = null;

    [SerializeField] private TMP_Text moneyText;

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

        moneyText.text = "0";
    }

    void OnEnable()
    {
        GameEvents.Money.OnMoneyChanged += HandleOnMoneyChanged;
    }

    void OnDisable()
    {
        GameEvents.Money.OnMoneyChanged -= HandleOnMoneyChanged;
    }

    private void HandleOnMoneyChanged(double amount)
    {
        moneyText.text = MoneyHelper.FormatMoney(amount);
    }
}
