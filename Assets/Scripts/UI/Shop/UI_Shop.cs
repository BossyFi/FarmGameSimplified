using System;
using System.Collections.Generic;
using Items;
using MoreMountains.Feedbacks;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.Shop
{
    public class UIShop : MonoBehaviour
    {
        [SerializeField] private Transform container;
        [SerializeField] private GameObject shopItemTemplate;
        [SerializeField] private Button closeBtn;
        [SerializeField] private MMF_Player shopMmfPlayer;

        public LinkedList<KeyValuePair<int, int>> shopItems;

        public UnityEvent<int> buyEvent;

        private UIMediator _mediator;

        private void Awake()
        {
            shopItems = new LinkedList<KeyValuePair<int, int>>();
            closeBtn.onClick.AddListener(Hide);
        }

        private void CreateShopItem(int itemCode)
        {
            //Check if the item already exists and if it doesnt, add it to the sorted list
            int itemPrize = ItemData.GetPrize(itemCode);
            string itemName = ItemData.GetName(itemCode);
            var pair = new KeyValuePair<int, int>(itemCode, itemPrize);
            int listIdx = 0;
            if (shopItems.Count == 0) shopItems.AddFirst(pair);
            else
            {
                for (var it = shopItems.First; it != null; it = it?.Next)
                {
                    if (it.Value.Value > itemPrize)
                    {
                        shopItems.AddBefore(it, pair);
                        break;
                    }

                    if (it.Value.Value == itemPrize)
                    {
                        int comparison = String.CompareOrdinal(itemName, ItemData.GetName(it.Value.Key));
                        if (comparison == 0) return;
                        if (comparison < 0)
                        {
                            shopItems.AddBefore(it, pair);
                            break;
                        }
                    }

                    if (it.Next != null)
                    {
                        listIdx++;
                        continue;
                    }
                    shopItems.AddLast(pair);
                    listIdx++;
                    break;

                }
            }

            //Instantiate the new UI element
            Transform newShopItem = Instantiate(shopItemTemplate.transform, container);

            //Configure the element according to the item
            newShopItem.Find("ItemName").GetComponent<TextMeshProUGUI>().SetText(itemName);
            newShopItem.Find("ItemPrize").GetComponent<TextMeshProUGUI>().SetText(itemPrize.ToString());
            newShopItem.Find("ItemSprite").GetComponent<Image>().sprite = ItemData.GetSprite(itemCode);
            
            //Place it sorted by prize
            newShopItem.SetSiblingIndex(listIdx);

            newShopItem.gameObject.SetActive(true);

            Button newItemBtn = newShopItem.GetComponent<Button>();
            if (newItemBtn)
            {
                newItemBtn.onClick.AddListener(() => buyEvent.Invoke(itemCode));
            }
            else
            {
                Debug.Log("Button not found! ");
            }
        }

        private void Buy(int gameItem)
        {
            buyEvent.Invoke(gameItem);
        }

        public void Configure(UIMediator mediator)
        {
            _mediator = mediator;
        }

        public void Show()
        {
            shopMmfPlayer.PlayFeedbacks();
        }

        public void Hide()
        {
            shopMmfPlayer.PlayFeedbacks();
        }

        //Testing

        [ContextMenu("Create items")]
        public void CreateItem1()
        {
            CreateShopItem(0);
            CreateShopItem(1);
            CreateShopItem(2);
        }
        [ContextMenu("Create items 2")]
        public void CreateItem2()
        {
            CreateShopItem(3);
            CreateShopItem(4);
        }
    }
}