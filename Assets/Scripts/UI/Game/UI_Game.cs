using Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Game
{
    public class UIGame : MonoBehaviour
    {
        [SerializeField] private Button shopBtn;
        [SerializeField] private Button inventoryBtn;
        [SerializeField] private TextMeshProUGUI moneyText;
    
        private UIMediator _mediator;

        private void Awake()
        {
            shopBtn.onClick.AddListener(() => _mediator.OpenShop());
            inventoryBtn.onClick.AddListener(() => _mediator.OpenInventory(0));
        }

        public void Configure(UIMediator uiMediator)
        {
            _mediator = uiMediator;
        }

        public void UpdateMoney(int moneyCount, int profit)
        {
            moneyText.SetText(moneyCount.ToString());
        }
    }
}
