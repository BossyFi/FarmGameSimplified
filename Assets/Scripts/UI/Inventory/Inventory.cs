using System;
using System.Collections.Generic;
using Items;
using UI.Shop;
using UnityEngine;

namespace UI.Inventory
{
    public class Inventory : MonoBehaviour
    {
        public static Inventory Instance;

        public Dictionary<int, int> foodItems;
        public Dictionary<int, int> toyItems;

        private void Awake()
        {
            //Singleton
            if (Instance != null && Instance != this) Destroy(gameObject);
            else Instance = this;

            foodItems = new Dictionary<int, int>();
            toyItems = new Dictionary<int, int>();
        }

        public void AddItem(int newItem)
        {
            switch (ItemData.GetItemType(newItem))
            {
                case GameItemType.Food:
                    if (foodItems.TryGetValue(newItem, out _)) foodItems[newItem]++;
                    else foodItems.Add(newItem, 1);
                    break;

                case GameItemType.Toy:
                    if (toyItems.TryGetValue(newItem, out _)) toyItems[newItem]++;
                    else toyItems.Add(newItem, 1);
                    break;

                default:
                    throw new Exception("Item type not defined");
            }
        }

        public bool RemoveItem(int rItem)
        {
            int n;
            switch (ItemData.GetItemType(rItem))
            {
                case GameItemType.Food:
                    if (foodItems.TryGetValue(rItem, out n))
                    {
                        if (n > 0)
                        {
                            foodItems[rItem]--;
                            return true;
                        }
                    }
                    break;

                case GameItemType.Toy:
                    if (toyItems.TryGetValue(rItem, out n))
                    {
                        if (n > 0)
                        {
                            toyItems[rItem]--;
                            return true;
                        }
                    }
                    break;
                
                default:
                    throw new Exception("Item type not defined");
            }

            return false;
        }

        [ContextMenu("Show inventory")]
        void ShowInventory()
        {
            Debug.Log("Food: ");
            foreach (KeyValuePair<int,int> food in foodItems)
            {
                Debug.Log(food.Key + ": " + food.Value);
            }
            Debug.Log("Toy: ");
            foreach (KeyValuePair<int,int> toy in toyItems)
            {
                Debug.Log(toy.Key + ": " + toy.Value);
            }
        }

        [ContextMenu("Remove 3")]
        void RemoveXOfEach()
        {
            for (int i = 0; i < 3; i++)
            {
                RemoveItem(0);
                RemoveItem(1);
                RemoveItem(2);
            }
        }
    }
}