using System;
using System.Collections.Generic;
using Items;
using MoreMountains.Feedbacks;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI.Inventory
{
    public class UIInventory : MonoBehaviour
    {
        public bool isOpen;

        [Space(1)] [Header("CONTAINERS")] 
        [SerializeField] private Transform foodContainer;
        [SerializeField] private Transform toyContainer;
        [SerializeField] private Transform medContainer;
        
        [Space(1)] [Header("ITEM TEMPLATE")] 
        [SerializeField] private GameObject inventoryItemTemplate;
        
        [Space(1)] [Header("MMF_PLAYER")]
        [SerializeField] private MMF_Player inventoryMmfPlayer;

        //Lists for sorting purpose
        private LinkedList<KeyValuePair<int, int>> _foodItems;
        private LinkedList<KeyValuePair<int, int>> _toyItems;
        private LinkedList<KeyValuePair<int, int>> _medItems;
        
        private UIMediator _mediator;

        private void Awake()
        {
            _foodItems = new LinkedList<KeyValuePair<int, int>>();
            _toyItems = new LinkedList<KeyValuePair<int, int>>();
            _medItems = new LinkedList<KeyValuePair<int, int>>();
        }


        public void Configure(UIMediator uiMediator)
        {
            _mediator = uiMediator;
        }

        public void ModifyUIItems(int itemCode, int itemCount)
        {
            switch (ItemData.GetItemType(itemCode))
            {
                case GameItemType.Food:
                    ModifyUIItemsDefault(ref _foodItems, ref foodContainer, itemCode, itemCount);
                    break;
                case GameItemType.Toy:
                    ModifyUIItemsDefault(ref _toyItems, ref toyContainer, itemCode, itemCount);
                    break;
                case GameItemType.Medicine:
                    ModifyUIItemsDefault(ref _medItems, ref medContainer, itemCode, itemCount);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void ModifyUIItemsDefault(
            ref LinkedList<KeyValuePair<int, int>> uiList, ref Transform container, int itemCode, int itemCount)
        {
            int idx = UIUtils.GetItemIdx(ref uiList, itemCode);
            if (itemCount == 0)
            {
                if (idx < 0) return;
                container.GetChild(idx).gameObject.SetActive(false);
            }
            else
            {
                if (idx >= 0)
                {
                    container.GetChild(idx).Find("ItemCount").GetComponent<TextMeshProUGUI>()
                        .SetText(itemCount.ToString());
                }
                else
                {
                    idx = UIUtils.AddItemSorted(ref uiList, itemCode);

                    Transform newInventoryItem = Instantiate(inventoryItemTemplate.transform, container);
                    UIUtils.ConfigureItemUI(ref newInventoryItem, itemCode, idx, itemCount);
                    newInventoryItem.gameObject.GetComponent<Button>().onClick.AddListener(() => AssignToDispenser(itemCode));
                }
            }
        }

        private void AssignToDispenser(int itemCode)
        {
            
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

        public void OpenClose()
        {
            inventoryMmfPlayer.PlayFeedbacks();
            isOpen = !isOpen;
        }

    }
}