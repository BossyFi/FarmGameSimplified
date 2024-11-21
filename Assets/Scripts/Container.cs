using Items;
using UI.Inventory;
using UI.Shop;
using UnityEngine;
using UnityEngine.UI;

public class Container : MonoBehaviour
{
    [SerializeField] private GameItem content;
    
  
    public void SetItemContainer(GameItem item)
    {
        content = item;
    }

    public bool UseItem()
    {
        if (Inventory.Instance.RemoveItem(content)) return true;
        Debug.Log("No item to use");
        return false;
    }
}