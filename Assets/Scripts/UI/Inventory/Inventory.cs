using System;
using System.Collections.Generic;
using UI.Shop;
using UnityEngine;

namespace UI.Inventory
{
    public class Inventory : MonoBehaviour
    {
        public static Inventory Instance;

        public List<GameItem> foodItems;
        public List<GameItem> toyItems;

        private void Awake()
        {
            //Singleton
            if (Instance != null && Instance != this) Destroy(gameObject);
            else Instance = this;

            foodItems = new List<GameItem>();
            toyItems = new List<GameItem>();
        }

        public void AddItem(GameItem newItem)
        {
            switch (ShopItem.GetItemType(newItem))
            {
                case GameItemType.Food:
                    foodItems.Add(newItem);
                    break;
                case GameItemType.Toy:
                    toyItems.Add(newItem);
                    break;
                default:
                    throw new Exception("Item type not defined");
            }
        }

    }
}