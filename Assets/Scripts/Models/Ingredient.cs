using UnityEngine;

public class Ingredient : MonoBehaviour
{
    [SerializeField] private GameObject popupAmountPrefab;
    [SerializeField] private StatType scalesWithStat;
    [SerializeField] private double baseValue = 0;

    void Start()
    {
        if (StatsManager.instance != null)
        {
            Stat stat = StatsManager.instance.GetStat(scalesWithStat);

            baseValue += stat.GetValue();
        }
    }
}