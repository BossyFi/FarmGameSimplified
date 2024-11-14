namespace UI.Shop
{
    public abstract class ShopItem
    {
        public enum ItemType
        {
            Item_1,
            Item_2,
            Item_3
        }

        public static int GetCost(ItemType item)
        {
            //Max price == 999999
            switch (item)
            {
                case ItemType.Item_1: return 10;
                case ItemType.Item_2: return 30;
                case ItemType.Item_3: return 50;
                default: return 0;
            }
        }
    }
}