using System;
using UnityEngine;

namespace Items
{
    
    public enum GameItem
    {
        Item1 = 0,
        Item2 = 1,
        Item3 = 2,
        Item4 = 3,
        Item5 = 4
    }

    public enum GameItemType
    {
        Food,
        Toy
    }
    
    public abstract class ItemData
    {

        public static string GetName(GameItem gameItem)
        {
            try
            {
                return ItemAssets.Instance.items[(int)gameItem].name;
            }
            catch
            {
                throw new ArgumentOutOfRangeException(nameof(gameItem), gameItem, 
                    "The selected item does not exist in ShopAssets");
            }
        }
        public static int GetPrize(GameItem gameItem)
        {
            try
            {
                return ItemAssets.Instance.items[(int)gameItem].itemPrize;
            }
            catch
            {
                throw new ArgumentOutOfRangeException(nameof(gameItem), gameItem, 
                    "The selected item does not exist in ShopAssets");
            }
        }

        public static Sprite GetSprite(GameItem gameItem)
        {
            try
            {
                return ItemAssets.Instance.items[(int)gameItem].itemSprite;
            }
            catch
            {
                throw new ArgumentOutOfRangeException(nameof(gameItem), gameItem, 
                    "The selected item does not exist in ShopAssets");
            }
        }

        public static GameItemType GetItemType(GameItem gameItem)
        {
            try
            {
                return ItemAssets.Instance.items[(int)gameItem].itemType;
            }
            catch
            {
                throw new ArgumentOutOfRangeException(nameof(gameItem), gameItem, 
                    "The selected item does not exist in ShopAssets");
            }
        }
        
    }
}