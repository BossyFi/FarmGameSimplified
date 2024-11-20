using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

namespace UI.Shop
{
    public class ShopAssets : MonoBehaviour
    {
        private static ShopAssets _instance;

        public static ShopAssets Instance
        {
            get
            {
                if (_instance == null) 
                    _instance = (Instantiate(Resources.Load("ShopAssets")) as GameObject)?.GetComponent<ShopAssets>();
                return _instance;
            }
        }
        
        [Space(1)] [Header("Item1")]
        public Sprite spriteItem1;
        public int item1Cost;
        public GameItemType item1Type;
        
        
        [Space(1)] [Header("Item2")]
        public Sprite spriteItem2;
        public int item2Cost;
        public GameItemType item2Type;

        
        [Space(1)] [Header("Item3")]
        public Sprite spriteItem3;
        public int item3Cost;
        public GameItemType item3Type;

        
        [Space(1)] [Header("Item4")]
        public Sprite spriteItem4;
        public int item4Cost;
        public GameItemType item4Type;

        
        [Space(1)] [Header("Item5")]
        public Sprite spriteItem5;
        public int item5Cost;
        public GameItemType item5Type;

        
    }
}
