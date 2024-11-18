using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Game
{
    public class UIGame : MonoBehaviour
    {
        [SerializeField] private Button shopBtn;
        [SerializeField] private TextMeshProUGUI moneyText;
    
        private UIMediator _mediator;

        private void Awake()
        {
            shopBtn.onClick.AddListener(() => _mediator.OpenShop());
        }

        public void Configure(UIMediator uiMediator)
        {
            _mediator = uiMediator;
        }

        public void UpdateMoney(int moneyCount)
        {
            moneyText.SetText(moneyCount.ToString());
        }
    }
}
