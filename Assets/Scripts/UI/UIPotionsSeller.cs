using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPotionsSeller : MonoBehaviour
{
    public static event Action<double> OnPotionSold;

    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private float sellTimerInterval = 3f;
    private float sellTimer;
    private float sellTimerImageWidth;

    [SerializeField] private int maxSellSlots = 1;
    [SerializeField] private List<PotionData> sellingPotions = new();

    [SerializeField] private Image potionSlot1;

    [SerializeField] private AudioClip sellSfx;

    void OnEnable()
    {
        Potion.OnPotionPickup += HandleOnPotionPickup;
    }

    void OnDisable()
    {
        Potion.OnPotionPickup -= HandleOnPotionPickup;
    }

    private void Start()
    {
        potionSlot1.transform.parent.gameObject.SetActive(false);
    }

    private void HandleOnPotionPickup(GameObject go)
    {
        if (sellingPotions.Count < maxSellSlots)
        {
            Potion potion = go.GetComponent<Potion>();

            sellingPotions.Add(potion.data);

            potionSlot1.transform.parent.gameObject.SetActive(true);

            Destroy(go);
        }
    }

    private void Awake()
    {
        sellTimerImageWidth = rectTransform.rect.width;
    }

    private void Update()
    {
        if (sellTimer > sellTimerInterval)
        {
            Sell();

            ResetVisualTimer();

            sellTimer = 0;
        }

        if (sellingPotions.Count > 0)
        {
            DecreaseVisualTimer();
            sellTimer += Time.deltaTime;
        }        
    }

    private void DecreaseVisualTimer()
    {
        float percent = Mathf.Clamp01(sellTimer / sellTimerInterval);

        rectTransform.sizeDelta = new Vector2(
            sellTimerImageWidth * (1f - percent),
            rectTransform.sizeDelta.y
        );
    }

    private void ResetVisualTimer()
    { 
        rectTransform.sizeDelta = new Vector2(sellTimerImageWidth, rectTransform.rect.height);
    }

    private void Sell()
    {
        if (sellingPotions.Count > 0)
        {
            PotionData potion = sellingPotions[0];

            Stat stat = StatsManager.instance.GetStat(potion.valueStatType);

            double amount = potion.baseValue + stat.GetValue();

            AudioManager.instance.PlayFX(sellSfx);

            OnPotionSold?.Invoke(amount);

            Debug.Log("Sold potion for: " + amount);

            sellingPotions.Remove(potion);

            potionSlot1.transform.parent.gameObject.SetActive(false);
        }
    }
}
