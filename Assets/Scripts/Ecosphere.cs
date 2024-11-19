using System;
using System.Collections.Generic;
using Animal;
using UI;
using UI.Shop;
using UnityEngine;

public class EcoSphere : MonoBehaviour
{
    public int money = 100;

    private bool _isOpen = true;
    public bool IsOpen
    {
        get => _isOpen;
        set
        {
            if (!_isOpen)
            {
                CollectionLoop();
                _isOpen = value;
            }
            else _isOpen = value;
        }
    }
    
    public float collectionT; //Seconds between each call collection 

    public List<AnimalBase> animals;

    private UIMediator _uiMediator;

    private void Start()
    {
        // moneyUpdatedEvent.Invoke(money);
        MoneyUpdateEvent.Trigger(money, 0);
        CollectionLoop();
    }

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
            // moneyUpdatedEvent.Invoke(money);
            MoneyUpdateEvent.Trigger(money, -cost);
        }
        
    }

    private async void CollectionLoop()
    {
        while (_isOpen)
        {
            await Awaitable.WaitForSecondsAsync(collectionT);
            int profit = CollectMoney();
            money += profit;
            // moneyUpdatedEvent.Invoke(money);
            MoneyUpdateEvent.Trigger(money, profit);
        }
    }

    private int CollectMoney()
    {
        double totalHappiness = 0;
        foreach (AnimalBase animal in animals)
        {
            totalHappiness += animal.happiness;
        }
        Debug.Log("Collected " + totalHappiness + "$ !");

        return (int)Math.Truncate(totalHappiness);
    }
}
