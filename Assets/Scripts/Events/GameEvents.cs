using System;

public static class GameEvents
{
    public static class Currency
    {
        public static Action OnCurrencyChanged;
    }

    public static class Player
    {
        public static Action OnDataReset;
    }

    public static class UI
    {
        public static Action OnTreePanningOrZooming;

        public static Action<string> OnUpgradeClicked;
    }
}