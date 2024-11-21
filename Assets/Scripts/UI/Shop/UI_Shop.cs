using System;
using System.Collections.Generic;
using Items;
using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.Shop
{
    public class UIShop : MonoBehaviour
    {
        public UnityEvent<int> buyEvent;
        public bool isOpen;

        [Space(1)] [Header("BUTTONS")] [SerializeField]
        private Button foodBtn;

        [SerializeField] private Button toyBtn;
        [SerializeField] private Button medBtn;
        [SerializeField] private Button closeBtn;

        [Space(1)] [Header("CONTAINERS")] [SerializeField]
        private Transform foodContainer;

        [SerializeField] private Transform toyContainer;
        [SerializeField] private Transform medContainer;

        [Space(1)] [Header("ITEM TEMPLATE")] [SerializeField]
        private GameObject shopItemTemplate;

        [Space(1)] [Header("MMF_PLAYER")] [SerializeField]
        private MMF_Player shopMmfPlayer;

        //Lists for sorting purpose
        private LinkedList<KeyValuePair<int, int>> _shopFoodItems;
        private LinkedList<KeyValuePair<int, int>> _shopToyItems;
        private LinkedList<KeyValuePair<int, int>> _shopMedItems;


        private UIMediator _mediator;


        private void Awake()
        {
            _shopFoodItems = new LinkedList<KeyValuePair<int, int>>();
            _shopToyItems = new LinkedList<KeyValuePair<int, int>>();
            _shopMedItems = new LinkedList<KeyValuePair<int, int>>();
            closeBtn.onClick.AddListener(Close);
            foodBtn.onClick.AddListener(() => ShowContainer(GameItemType.Food));
            toyBtn.onClick.AddListener(() => ShowContainer(GameItemType.Toy));
            medBtn.onClick.AddListener(() => ShowContainer(GameItemType.Medicine));
            ShowContainer(GameItemType.Food);
        }

        private void CreateShopItem(int itemCode)
        {
            switch (ItemData.GetItemType(itemCode))
            {
                case GameItemType.Food:
                    CreateShopItemDefault(itemCode, ref _shopFoodItems, ref foodContainer);
                    break;
                case GameItemType.Toy:
                    CreateShopItemDefault(itemCode, ref _shopToyItems, ref toyContainer);
                    break;
                case GameItemType.Medicine:
                    CreateShopItemDefault(itemCode, ref _shopMedItems, ref medContainer);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        private void CreateShopItemDefault(int itemCode, ref LinkedList<KeyValuePair<int, int>> shopItems,
            ref Transform container)
        {
            //Check if the item already exists and if it doesnt, add it to the sorted list
            int listIdx = UIUtils.AddItemSorted(ref shopItems, itemCode);
            if (listIdx < 0) return;

            //Instantiate the new UI element
            Transform newShopItem = Instantiate(shopItemTemplate.transform, container);

            //Configure the element according to the item
            UIUtils.ConfigureItemUI(ref newShopItem, itemCode, listIdx);

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

        public void Open()
        {
            if (isOpen) return;
            shopMmfPlayer.PlayFeedbacks();
            isOpen = true;
        }

        public void Close()
        {
            if(!isOpen) return;
            shopMmfPlayer.PlayFeedbacks();
            isOpen = false;
        }
        
        public void ShowContainer(GameItemType containerType)
        {
            switch (containerType)
            {
                case GameItemType.Food:
                    foodContainer.gameObject.SetActive(true);
                    toyContainer.gameObject.SetActive(false);
                    medContainer.gameObject.SetActive(false);
                    break;
                case GameItemType.Toy:
                    foodContainer.gameObject.SetActive(false);
                    toyContainer.gameObject.SetActive(true);
                    medContainer.gameObject.SetActive(false);
                    break;
                case GameItemType.Medicine:
                    foodContainer.gameObject.SetActive(false);
                    toyContainer.gameObject.SetActive(false);
                    medContainer.gameObject.SetActive(true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(containerType), containerType, null);
            }
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