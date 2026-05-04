using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance = null;

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
        GameplayManager.OnUpdateMoneyUI += HandleOnUpdateMoneyUI;
    }

    void OnDisable()
    {
        GameplayManager.OnUpdateMoneyUI -= HandleOnUpdateMoneyUI;
    }

    private void HandleOnUpdateMoneyUI(int totalMoney)
    {
        moneyText.text = totalMoney.ToString();
    }
}
