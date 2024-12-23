using System;
using Items;
using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using UI.Game;
using UI.Inventory;
using UI.Shop;
using Unity.VisualScripting;
using UnityEngine;

namespace UI
{
    public class UIMediator : MonoBehaviour, MMEventListener<MoneyUpdateEvent>, MMEventListener<InventoryUpdate>
    {
        [SerializeField] private UIGame gameUI;
        [SerializeField] private UIShop shopUI;
        [SerializeField] private UIInventory inventoryUI;
        private EcoSphere _player;

        private void OnEnable()
        {
            this.MMEventStartListening<MoneyUpdateEvent>();
            this.MMEventStartListening<InventoryUpdate>();
        }

        private void Awake()
        {
            gameUI.Configure(this);
            shopUI.Configure(this);
            inventoryUI.Configure(this);

            _player = GameObject.FindGameObjectWithTag("Player").GetComponent<EcoSphere>();
            shopUI.buyEvent.AddListener(_player.BuyItem);
        }

        private void OnDisable()
        {
            this.MMEventStopListening<MoneyUpdateEvent>();
            this.MMEventStopListening<InventoryUpdate>();
        }

        public void OpenShop()
        {
            if(inventoryUI.isOpen) inventoryUI.OpenClose();
            shopUI.OpenClose();
        }

        public void OpenInventory(int cont)
        {
            if(shopUI.isOpen) shopUI.OpenClose();
            
            switch (cont)
            {
                case 0:
                    inventoryUI.ShowContainer(GameItemType.Food);
                    break;
                
                case 1:
                    inventoryUI.ShowContainer(GameItemType.Toy);
                    break;
                
                case 2:
                    inventoryUI.ShowContainer(GameItemType.Medicine);
                    break;
                
                default:
                    inventoryUI.ShowContainer(GameItemType.Food);
                    break;
                
            }
            inventoryUI.OpenClose();
        }

        public void UpdateMoney(int moneyCount, int profit)
        {
            gameUI.UpdateMoney(moneyCount, profit);
        }

        public void OnMMEvent(MoneyUpdateEvent moneyMessage)
        {
            UpdateMoney(moneyMessage.MoneyAmount, moneyMessage.Profit);
        }

        public void OnMMEvent(InventoryUpdate inventoryUpdate)
        {
            inventoryUI.ModifyUIItems(inventoryUpdate.ItemCode, inventoryUpdate.ItemCount);
        }
    }
}