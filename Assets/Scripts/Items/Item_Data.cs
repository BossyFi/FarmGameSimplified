using System;
using UnityEngine;

namespace Items
{
    

    public enum GameItemType
    {
        Food = 0,
        Toy = 1,
        Medicine = 2
    }
    
    public abstract class ItemData
    {

        public static string GetName(int itemCode)
        {
            try
            {
                return ItemAssets.Instance.items[itemCode].itemName;
            }
            catch
            {
                throw new ArgumentOutOfRangeException(nameof(itemCode), itemCode, 
                    "The selected item does not exist in ShopAssets");
            }
        }
        public static int GetPrize(int itemCode)
        {
            try
            {
                return ItemAssets.Instance.items[itemCode].itemPrize;
            }
            catch
            {
                throw new ArgumentOutOfRangeException(nameof(itemCode), itemCode, 
                    "The selected item does not exist in ShopAssets");
            }
        }

        public static Sprite GetSprite(int itemCode)
        {
            try
            {
                return ItemAssets.Instance.items[itemCode].itemSprite;
            }
            catch
            {
                throw new ArgumentOutOfRangeException(nameof(itemCode), itemCode, 
                    "The selected item does not exist in ShopAssets");
            }
        }

        public static GameItemType GetItemType(int itemCode)
        {
            try
            {
                return ItemAssets.Instance.items[itemCode].itemType;
            }
            catch
            {
                throw new ArgumentOutOfRangeException(nameof(itemCode), itemCode, 
                    "The selected item does not exist in ShopAssets");
            }
        }
        
    }
}