using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "Item_Scriptable", menuName = "Scriptable Objects/Item_Scriptable")]
    public class ItemScriptable : ScriptableObject
    {
        public string name;
        public Sprite itemSprite;
        public int itemPrize;
        public GameItemType itemType;
    }
}
