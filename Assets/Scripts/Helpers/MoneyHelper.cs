public static class MoneyHelper
{
    public static string FormatMoney(double amount)
    {
        string[] suffixes = { "", "K", "M", "B", "T", "Qa", "Qi", "Sx", "Sp", "Oc", "No", "Dc" };

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

        return tempMoney.ToString("0.##") + suffixes[suffixIndex];
    }
}
