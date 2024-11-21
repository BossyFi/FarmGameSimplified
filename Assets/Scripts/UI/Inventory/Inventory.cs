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
        public Dictionary<int, int> medItems;

        private void Awake()
        {
            //Singleton
            if (Instance != null && Instance != this) Destroy(gameObject);
            else Instance = this;

            foodItems = new Dictionary<int, int>();
            toyItems = new Dictionary<int, int>();
            medItems = new Dictionary<int, int>();
        }

        public void AddItem(int newItem)
        {
            switch (ItemData.GetItemType(newItem))
            {
                case GameItemType.Food:
                    AddItemDefault(ref foodItems, newItem);
                    break;

                case GameItemType.Toy:
                    AddItemDefault(ref toyItems, newItem);
                    break;

                case GameItemType.Medicine:
                    AddItemDefault(ref medItems, newItem);
                    break;

                default:
                    throw new Exception("Item type not defined");
            }
        }

        private void AddItemDefault(ref Dictionary<int, int> itemDic, int itemCode)
        {
            if (itemDic.TryGetValue(itemCode, out var itemCount)) itemDic[itemCode]++;
            else itemDic.Add(itemCode, 1);
            InventoryUpdate.Trigger(itemCode, itemCount + 1, itemCount);
        }

        public bool RemoveItem(int rItem)
        {
            return ItemData.GetItemType(rItem) switch
            {
                GameItemType.Food => RemoveItemDefault(foodItems, rItem),
                GameItemType.Toy => RemoveItemDefault(toyItems, rItem),
                GameItemType.Medicine => RemoveItemDefault(medItems, rItem),
                _ => throw new Exception("Item type not defined")
            };
        }

        private bool RemoveItemDefault(Dictionary<int, int> itemDic, int itemCode)
        {
            if (itemDic.TryGetValue(itemCode, out var itemCount))
            {
                if (itemCount > 0)
                {
                    itemDic[itemCode]--;
                    InventoryUpdate.Trigger(itemCode, itemCount - 1, itemCount);
                    return true;
                }
            }

            return false;
        }

        [ContextMenu("Show inventory")]
        void ShowInventory()
        {
            Debug.Log("Food: ");
            foreach (KeyValuePair<int, int> food in foodItems)
            {
                Debug.Log(food.Key + ": " + food.Value);
            }

            Debug.Log("Toy: ");
            foreach (KeyValuePair<int, int> toy in toyItems)
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