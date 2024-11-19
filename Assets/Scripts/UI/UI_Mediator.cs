using System;
using MoreMountains.Tools;
using UI.Game;
using UI.Shop;
using UnityEngine;

namespace UI
{

    public class UIMediator : MonoBehaviour, MMEventListener<MoneyUpdateEvent>
    {
        [SerializeField] private UIGame gameUI;
        [SerializeField] private UIShop shopUI;
        private EcoSphere _player;

        private void OnEnable()
        {
            this.MMEventStartListening();
        }

        private void Awake()
        {
            gameUI.Configure(this);
            shopUI.Configure(this);
            
            _player = GameObject.FindGameObjectWithTag("Player").GetComponent<EcoSphere>();
            shopUI.buyEvent.AddListener(_player.BuyItem);
        }

        private void OnDisable()
        {
            this.MMEventStopListening();
        }

        public void OpenShop()
        {
            shopUI.Show();
        }

        public void UpdateMoney(int moneyCount, int profit)
        {
            gameUI.UpdateMoney(moneyCount, profit);
        }

        public void OnMMEvent(MoneyUpdateEvent moneyMessage)
        {
            UpdateMoney(moneyMessage.MoneyAmount, moneyMessage.Profit);
        }
    }
}
