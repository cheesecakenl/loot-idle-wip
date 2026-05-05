using DG.Tweening;
using TMPro;
using UnityEngine;

public class Chest : MonoBehaviour, IClickable
{
    [SerializeField] private GameObject bronzePrefab;
    [SerializeField] private GameObject silverPrefab;
    [SerializeField] private GameObject goldPrefab;

    [SerializeField] private int hitPoints = 3;
    [SerializeField] private float animationShakeDuration = 0.3f;
    [SerializeField] private float animationShakeStrength = 0.2f;
    [SerializeField] private GameObject popupAmountPrefab;
    [SerializeField] private GameObject chestOpenPrefab;
    [SerializeField] private float fadeDuration = 1f;

    private bool isDead = false;

    private Rigidbody2D rb;
    private Vector3 startingScale;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 3f;

        startingScale = transform.localScale;
    }

    void OnMouseDown()
    {
        Click();
    }

    public void Click()
    {
        if (isDead) return;

        PlaySFX();
        Animate();
        //ShowAmountPopup();

        hitPoints--;

        if (hitPoints < 1)
        {
            Open();
        }
    }

    void Open()
    {
        isDead = true;

        SpawnLoot();

        FadeAway();

        Destroy(gameObject);
    }

    void Animate()
    {
        transform.DOKill();
        transform.localScale = startingScale;
        transform.DOShakeScale(animationShakeDuration, strength: animationShakeStrength);
    }

    void FadeAway()
    {
        GameObject clone = Instantiate(chestOpenPrefab, transform.position, Quaternion.identity);

        clone.transform.DOKill();
        clone.transform.localScale = startingScale;
        clone.transform.GetComponent<SpriteRenderer>().DOFade(0f, fadeDuration);

        Destroy(clone, fadeDuration);
    }

    void PlaySFX()
    {
        AudioManager.instance.PlayFX(AudioType.SFX, "hit");
    }

    void SpawnLoot()
    {
        int amount = Random.Range(1, 6);

        // Fix so Coin OnMouseOver is in front of chest collider
        Vector3 position = new Vector3(transform.position.x, transform.position.y, -1f);

        for (int i = 0; i < amount; i++)
        {
            GameObject clone = Instantiate(bronzePrefab, position, Quaternion.identity);
            clone.GetComponent<CoinPhysics>().Launch();
        }

        amount = Random.Range(1, 6);

        for (int i = 0; i < amount; i++)
        {
            GameObject clone = Instantiate(silverPrefab, position, Quaternion.identity);
            clone.GetComponent<CoinPhysics>().Launch();
        }

        amount = Random.Range(1, 6);

        for (int i = 0; i < amount; i++)
        {
            GameObject clone = Instantiate(goldPrefab, position, Quaternion.identity);
            clone.GetComponent<CoinPhysics>().Launch();
        }
    }

    void ShowAmountPopup()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        GameObject clone = Instantiate(popupAmountPrefab, mousePos, Quaternion.identity);
        TMP_Text amountText = clone.GetComponentInChildren<TMP_Text>();

        amountText.text = "1";
        amountText.color = Color.red;
    }
}