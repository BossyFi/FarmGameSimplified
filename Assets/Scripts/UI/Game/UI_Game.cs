using UnityEngine;
using UnityEngine.UI;

namespace UI.Game
{
    public class UIGame : MonoBehaviour
    {
        [SerializeField] private Button shopBtn;
    
        private UIMediator _mediator;

        private void Awake()
        {
            shopBtn.onClick.AddListener(() => _mediator.OpenShop());
        }

        public void Configure(UIMediator uiMediator)
        {
            _mediator = uiMediator;
        }
    }
}
