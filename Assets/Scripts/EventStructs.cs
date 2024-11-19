using MoreMountains.Tools;
using UnityEngine;


public struct MoneyUpdateEvent
{
    public int MoneyAmount;
    public int Profit;

    private static MoneyUpdateEvent _moneyMessage;

    public static void Trigger(int m, int p)
    {
        _moneyMessage.MoneyAmount = m;
        _moneyMessage.Profit = p;
        MMEventManager.TriggerEvent(_moneyMessage);
    }
}

