using System;
using UnityEngine;

namespace UI.Shop
{
    
    public enum GameItem
    {
        Item1,
        Item2,
        Item3
    }

    public enum GameItemType
    {
        Food,
        Toy
    }
    
    public abstract class ShopItem
    {
        public GameItemType Type;

        public static int GetCost(GameItem gameItem)
        {
            //Max price == 999999
            switch (gameItem)
            {
                case GameItem.Item1: 
                    return ShopAssets.Instance.item1Cost;
                case GameItem.Item2: 
                    return ShopAssets.Instance.item2Cost;
                case GameItem.Item3: 
                    return ShopAssets.Instance.item3Cost;
                default: return 0;
            }
        }

        public static Sprite GetSprite(GameItem gameItem)
        {
            switch (gameItem)
            {
                case GameItem.Item1: 
                    return ShopAssets.Instance.spriteItem1;
                case GameItem.Item2: 
                    return ShopAssets.Instance.spriteItem2;
                case GameItem.Item3: 
                    return ShopAssets.Instance.spriteItem3;
                default: 
                    return null;
            }
        }

        public static GameItemType GetItemType(GameItem gameItem)
        {
            switch (gameItem)
            {
                case GameItem.Item1:
                    return ShopAssets.Instance.item1Type;
                case GameItem.Item2:
                    return ShopAssets.Instance.item2Type;
                case GameItem.Item3:
                    return ShopAssets.Instance.item3Type;
                default:
                    throw new ArgumentOutOfRangeException(nameof(gameItem), gameItem, null);
            }
        }
        
    }
}