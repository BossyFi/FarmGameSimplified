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

public struct InventoryUpdate
{
    public int ItemCode;
    public int ItemCount;
    public int ItemPrevCount;

    private static InventoryUpdate _inventoryUpdate;

    public static void Trigger(int code, int count, int prevCount)
    {
        _inventoryUpdate.ItemCode = code;
        _inventoryUpdate.ItemCount = count;
        _inventoryUpdate.ItemPrevCount = prevCount;
        MMEventManager.TriggerEvent(_inventoryUpdate);
    }
}

