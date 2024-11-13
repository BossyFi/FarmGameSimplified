using UnityEngine;

public class Item
{
    public enum ItemType
    {
        Item_1,
        Item_2,
        Item_3
    }

    public static int GetCost(ItemType item)
    {
        switch (item)
        {
            case ItemType.Item_1: return 10;
            case ItemType.Item_2: return 30;
            case ItemType.Item_3: return 50;
            default: return 0;
        }
    }
}