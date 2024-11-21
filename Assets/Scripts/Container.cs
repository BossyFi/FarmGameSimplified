using Items;
using UI.Inventory;
using UI.Shop;
using UnityEngine;
using UnityEngine.UI;

public class Container : MonoBehaviour
{
    [SerializeField] private int content;
    
  
    public void SetItemContainer(int item)
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