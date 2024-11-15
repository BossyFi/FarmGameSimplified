using UnityEngine;

namespace UI.Shop
{
    public abstract class ShopItem
    {
        public enum ItemType
        {
            Item1,
            Item2,
            Item3
        }

        public static int GetCost(ItemType item)
        {
            //Max price == 999999
            switch (item)
            {
                case ItemType.Item1: return 10;
                case ItemType.Item2: return 30;
                case ItemType.Item3: return 50;
                default: return 0;
            }
        }

        public static Sprite GetSprite(ItemType item)
        {
            switch (item)
            {
                case ItemType.Item1: return ShopAssets.Instance.spriteItem1;
                case ItemType.Item2: return ShopAssets.Instance.spriteItem2;
                case ItemType.Item3: return ShopAssets.Instance.spriteItem3;
                default: return null;
            }
        }
    }
}