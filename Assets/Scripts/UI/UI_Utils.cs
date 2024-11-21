using System;
using System.Collections.Generic;
using Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public static class UIUtils
    {
        /// <summary>
        /// Adds an item the the sorted LinkedList in ascending prize order.
        /// If the item already exists it is not inserted.
        /// It the item prize already exist it is alphabetically sorted by the item name.
        /// </summary>
        /// <param name="list"> A list of KeyValuePair {itemCode, itemPrize} </param>
        /// <param name="itemCode">The code of the inserted item</param>
        /// <returns>The item index. -1 if the item was not added</returns>
        public static int AddItemSorted(ref LinkedList<KeyValuePair<int, int>> list, int itemCode)
        {
            int idx = 0;
            int itemPrize = ItemData.GetPrize(itemCode);
            string itemName = ItemData.GetName(itemCode);
            KeyValuePair<int, int> itemPair = new KeyValuePair<int, int>(itemCode, itemPrize);
        
            if (list.Count == 0) list.AddFirst(itemPair);
            else
            {
                for (var it = list.First; it != null; it = it?.Next)
                {
                    if (it.Value.Value > itemPrize)
                    {
                        list.AddBefore(it, itemPair);
                        break;
                    }

                    if (it.Value.Value == itemPrize)
                    {
                        int comparison = String.CompareOrdinal(itemName, ItemData.GetName(it.Value.Key));
                        if (comparison == 0) return -1;
                        if (comparison < 0)
                        {
                            list.AddBefore(it, itemPair);
                            break;
                        }
                    }

                    if (it.Next != null)
                    {
                        idx++;
                        continue;
                    }
                    list.AddLast(itemPair);
                    idx++;
                    break;

                }
            }
            
            return idx;
        }

        /// <summary>
        /// Gets the position of an item inside a LinkedList.
        /// </summary>
        /// <param name="list">A list of KeyValuePair {itemCode, itemPrize}</param>
        /// <param name="itemCode">The code of the item whose index is being searched</param>
        /// <returns>The item position in the sorted list. -1 if the item does not exist</returns>
        public static int GetItemIdx(ref LinkedList<KeyValuePair<int, int>> list, int itemCode)
        {
            int idx = 0;
            for (var it = list.First; it != null; it = it?.Next)
            {
                if (it.Value.Key == itemCode) return idx;
                idx++;
            }

            return -1;
        }

        /// <summary>
        /// Configures an item UI parameters according to the item data.
        /// </summary>
        /// <param name="uiItem">The transform of the new Item UI</param>
        /// <param name="itemCode">The code of the item</param>
        /// <param name="idx">The item index inside its container</param>
        /// <param name="itemCount">The amount of items of this type. Only needed in the inventory</param>
        public static void ConfigureItemUI(ref Transform uiItem, int itemCode,int idx, int itemCount = -1)
        {
            int itemPrize = ItemData.GetPrize(itemCode);
            string itemName = ItemData.GetName(itemCode);
            //Configure the element according to the item
            uiItem.Find("ItemName").GetComponent<TextMeshProUGUI>().SetText(itemName);
            uiItem.Find("ItemSprite").GetComponent<Image>().sprite = ItemData.GetSprite(itemCode);
            if(itemCount != -1 )
                uiItem.Find("ItemCount").GetComponent<TextMeshProUGUI>().SetText(itemCount.ToString());
            else
                uiItem.Find("ItemPrize").GetComponent<TextMeshProUGUI>().SetText(itemPrize.ToString());

            //Place it sorted by prize
            uiItem.SetSiblingIndex(idx);

            uiItem.gameObject.SetActive(true);
        }
    }
}
