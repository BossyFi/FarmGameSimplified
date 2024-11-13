using System;
using UnityEngine;
using UnityEngine.Events;

[ExecuteInEditMode]
public class Shop : MonoBehaviour
{
    public UnityEvent<Item.ItemType> buyEvent;

    //Testing
    [ContextMenu("Item 1")]
    public void BuyItem1(){ buyEvent.Invoke(Item.ItemType.Item_1); }
    [ContextMenu("Item 2")]
    public void BuyItem2(){ buyEvent.Invoke(Item.ItemType.Item_2); }
    [ContextMenu("Item 3")]
    public void BuyItem3(){ buyEvent.Invoke(Item.ItemType.Item_3); }

}
