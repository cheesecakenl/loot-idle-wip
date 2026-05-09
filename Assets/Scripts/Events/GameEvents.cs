using System;
using UnityEngine;

public static class GameEvents
{
    public static class Money
    {
        public static Action<double> OnMoneyChanged;
    }

    public static class Potion
    {
        public static Action<GameObject> OnPotionPickup;
        public static Action<double> OnPotionSold;
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