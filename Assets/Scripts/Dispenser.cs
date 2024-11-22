using Items;
using UI.Inventory;
using UI.Shop;
using UnityEngine;
using UnityEngine.UI;

public class Dispenser : MonoBehaviour
{
    [SerializeField] private int content;
    
  
    public void SetItemContainer(int itemCode)
    {
        content = itemCode;
    }

    public bool UseItem()
    {
        if (Inventory.Instance.RemoveItem(content)) return true;
        Debug.Log("No item to use");
        return false;
    }
}