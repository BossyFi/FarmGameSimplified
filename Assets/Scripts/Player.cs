using System;
using UI.Shop;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int money = 100;

    public void BuyItem(ShopItem.ItemType item)
    {
        int cost = ShopItem.GetCost(item);
        if (cost > money)
        {
            Debug.Log("Unable to buy Item");
        }
        else
        {
            money -= cost;
            Debug.Log("Bought item " + item + " for " + cost + ". Remaining: " + money);
        }
        
    }

}
