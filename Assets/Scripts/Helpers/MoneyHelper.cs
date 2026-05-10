using System;
using UnityEngine;

public static class MoneyHelper
{
    private static readonly string[] suffixes = {
        "",
        "K",
        "M",
        "B",
        "T",
        "Qa", // quadrillion
        "Qi", // quintillion
        "Sx", // sextillion
        "Sp", // septillion
        "Oc", // octillion
        "No", // nonillion
        "Dc"  // decillion
    };

    public static string FormatMoney(double amount)
    {
        double tempMoney = amount;

        if (tempMoney < 1000)
            return tempMoney.ToString("0");

        int suffixIndex = 0;

        while (tempMoney >= 1000 && suffixIndex < suffixes.Length - 1)
        {
            tempMoney /= 1000;
            suffixIndex++;
        }

        // Fallback to scientific notation if we ran out of suffixes
        if (suffixIndex == suffixes.Length - 1 && tempMoney >= 1000)
        {
            return tempMoney.ToString("0.000e+0");
        }

        return tempMoney.ToString("0.##") + " " + suffixes[suffixIndex];
    }

    public static double CalculatePrestigePoints(double totalMoneyCurrentRun)
    {
        // Lower number is faster prestige points. Higher number is slower. 1e6 = 1000.000
        double points = Math.Floor(Math.Sqrt(totalMoneyCurrentRun / 1e6));

        Debug.Log($"Presige points: {points}");

        return points;
    }
}
