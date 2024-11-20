using System;
using System.Collections.Generic;
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

        public LinkedList<KeyValuePair<GameItem, int>> shopItems;

        public UnityEvent<GameItem> buyEvent;

        private UIMediator _mediator;

        private void Awake()
        {
            shopItems = new LinkedList<KeyValuePair<GameItem, int>>();
            closeBtn.onClick.AddListener(Hide);
        }

        private void CreateShopItem(GameItem newItem)
        {
            //Check if the item already exists and if it doesnt, add it to the sorted list
            int prize = ShopItem.GetPrize(newItem);
            var pair = new KeyValuePair<GameItem, int>(newItem, prize);
            int list_idx = 0;
            if (shopItems.Count == 0) shopItems.AddFirst(pair);
            else
            {
                for (var it = shopItems.First; it != null; it = it?.Next)
                {
                    if (it.Value.Value > prize)
                    {
                        shopItems.AddBefore(it, pair);
                        break;
                    }

                    if (it.Value.Value == prize)
                    {
                        int comparison = String.CompareOrdinal(newItem.ToString(), it.Value.Key.ToString());
                        if (comparison == 0) return;
                        if (comparison < 0)
                        {
                            shopItems.AddBefore(it, pair);
                            break;
                        }
                    }

                    if (it.Next != null)
                    {
                        list_idx++;
                        continue;
                    }
                    shopItems.AddLast(pair);
                    list_idx++;
                    break;

                }
            }

            //Instantiate the new UI element
            Transform newShopItem = Instantiate(shopItemTemplate.transform, container);

            //Configure the element according to the item
            newShopItem.Find("ItemName").GetComponent<TextMeshProUGUI>().SetText(newItem.ToString());
            newShopItem.Find("ItemPrize").GetComponent<TextMeshProUGUI>().SetText(prize.ToString());
            newShopItem.Find("ItemSprite").GetComponent<Image>().sprite =
                ShopItem.GetSprite(newItem);
            newShopItem.SetSiblingIndex(list_idx);

            newShopItem.gameObject.SetActive(true);

            Button newItemBtn = newShopItem.GetComponent<Button>();
            if (newItemBtn)
            {
                newItemBtn.onClick.AddListener(() => buyEvent.Invoke(newItem));
            }
            else
            {
                Debug.Log("Button not found! ");
            }
        }

        private void Buy(GameItem gameItem)
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
            CreateShopItem(GameItem.Item1);
            CreateShopItem(GameItem.Item2);
            CreateShopItem(GameItem.Item3);
        }
        [ContextMenu("Create items 2")]
        public void CreateItem2()
        {
            CreateShopItem(GameItem.Item4);
            CreateShopItem(GameItem.Item5);
        }
    }
}