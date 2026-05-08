using System;
using UnityEngine;
using UnityEngine.UI;

public class UISeller : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private float sellTimerInterval = 3f;
    private float sellTimer;

    private float sellTimerImageWidth;

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

        DecreaseVisualTimer();

        sellTimer += Time.deltaTime;
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
        //throw new NotImplementedException();
    }
}
