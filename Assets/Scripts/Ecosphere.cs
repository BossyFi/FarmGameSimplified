using System;
using System.Collections.Generic;
using Animal;
using UI;
using UI.Shop;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class EcoSphere : MonoBehaviour
{
    public int money = 100;
    [FormerlySerializedAs("moneyEarnedEvent")] public UnityEvent<int> moneyUpdatedEvent;

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

    private void Awake()
    {
        moneyUpdatedEvent.Invoke(money);
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
            moneyUpdatedEvent.Invoke(money);
        }
        
    }

    private async void CollectionLoop()
    {
        while (_isOpen)
        {
            await Awaitable.WaitForSecondsAsync(collectionT);
            money += CollectMoney();
            moneyUpdatedEvent.Invoke(money);
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
