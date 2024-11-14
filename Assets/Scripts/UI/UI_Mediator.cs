using UI.Game;
using UI.Shop;
using UnityEngine;

namespace UI
{
    public class UIMediator : MonoBehaviour
    {
        [SerializeField] private UIGame gameUI;
        [SerializeField] private UIShop shopUI;

        private void Awake()
        {
        
            gameUI.Configure(this);
            shopUI.Configure(this);
        }

        public void OpenShop()
        {
            shopUI.Show();
        }
    }
}
