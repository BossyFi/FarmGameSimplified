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
        
        public Sprite spriteItem1;
        public Sprite spriteItem2;
        public Sprite spriteItem3;
    }
}
