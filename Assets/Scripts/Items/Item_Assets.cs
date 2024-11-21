using System.Collections.Generic;
using UI.Shop;
using UnityEngine;

namespace Items
{
    public class ItemAssets : MonoBehaviour
    {
        private static ItemAssets _instance;

        public static ItemAssets Instance
        {
            get
            {
                if (_instance == null) 
                    _instance = (Instantiate(Resources.Load("ShopAssets")) as GameObject)?.GetComponent<ItemAssets>();
                return _instance;
            }
        }

        public List<ItemScriptable> items;
        
    }
}
