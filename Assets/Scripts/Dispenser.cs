using System;
using Items;
using UI;
using UI.Inventory;
using UI.Shop;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Dispenser : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameItemType itemType;
    private int _content = -1;
    
    public void SetItemContainer(int itemCode)
    {
        _content = itemCode;
    }

    public bool UseItem()
    {
        if (Inventory.Instance.RemoveItem(_content)) return true;
        EcoSphere.Instance.SetActiveDispenser(this);
        GameObject.FindGameObjectWithTag("UI").GetComponent<UIMediator>().OpenInventory((int)itemType);
        Debug.Log("Empty dispenser");
        return false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        EcoSphere.Instance.SetActiveDispenser(this);
    }
}