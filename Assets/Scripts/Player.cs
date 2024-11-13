using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int money = 100;

    public void BuyItem(Item.ItemType item)
    {
        int cost = Item.GetCost(item);
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
