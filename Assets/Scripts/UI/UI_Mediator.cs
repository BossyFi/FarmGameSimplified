using UI.Game;
using UI.Shop;
using UnityEngine;

namespace UI
{
    public class UIMediator : MonoBehaviour
    {
        [SerializeField] private UIGame gameUI;
        [SerializeField] private UIShop shopUI;
        private EcoSphere _player;

        private void Awake()
        {
            gameUI.Configure(this);
            shopUI.Configure(this);
            
            _player = GameObject.FindGameObjectWithTag("Player").GetComponent<EcoSphere>();
            shopUI.buyEvent.AddListener(_player.BuyItem);
            _player.moneyUpdatedEvent.AddListener(UpdateMoney);
        }

        public void OpenShop()
        {
            shopUI.Show();
        }

        public void UpdateMoney(int moneyCount)
        {
            gameUI.UpdateMoney(moneyCount);
        }
    }
}
