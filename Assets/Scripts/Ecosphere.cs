using System;
using System.Collections.Generic;
using Animal;
using Items;
using UI;
using UI.Inventory;
using UI.Shop;
using UnityEngine;

public class EcoSphere : MonoBehaviour
{
    public static EcoSphere Instance;
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

    public Dispenser activeDispenser;
    
    //For Debug
    public List<AnimalBase> animals;


    private void Awake()
    {
        if (Instance != null) Destroy(this.gameObject);
        else Instance = this;
        
        activeDispenser = null;
    }

    private void Start()
    {
        // moneyUpdatedEvent.Invoke(money);
        MoneyUpdateEvent.Trigger(money, 0);
        CollectionLoop();
    }

    public void BuyItem(int gameItem)
    {
        int cost = ItemData.GetPrize(gameItem);
        if (cost > money)
        {
            Debug.Log("Unable to buy Item");
        }
        else
        {
            money -= cost;
            MoneyUpdateEvent.Trigger(money, -cost);
            Inventory.Instance.AddItem(gameItem);
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

        return (int)Math.Truncate(totalHappiness);
    }

    public void SetActiveDispenser(Dispenser dispenser = null)
    {
        activeDispenser = dispenser;
    }
}