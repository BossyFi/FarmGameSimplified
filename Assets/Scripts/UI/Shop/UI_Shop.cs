using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI.Shop
{
    public class UIShop : MonoBehaviour
    {
        [SerializeField] private Transform container;
        [SerializeField] private Transform shopItemTemplate;
        [SerializeField] private Button closeBtn;
        
        private CanvasGroup _canvasGroup;

    
        public UnityEvent<ShopItem.ItemType> buyEvent;

        private UIMediator _mediator;

        private void Awake()
        {
            buyEvent.AddListener(GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().BuyItem);
            _canvasGroup = transform.Find("Canvas").GetComponent<CanvasGroup>();
            container = _canvasGroup.gameObject.transform.Find("Container");
            shopItemTemplate = container.Find("ShopItemTemplate");
            shopItemTemplate.gameObject.SetActive(false);
            closeBtn = _canvasGroup.gameObject.transform.Find("CloseBtn").GetComponent<Button>();
            closeBtn.onClick.AddListener(Hide);
            Hide();
        }

        private void CreateShopItem(ShopItem.ItemType type)
        {
            //Instantiate the new UI element
            Transform newShopItem = Instantiate(shopItemTemplate, container);
            // RectTransform shopItemRT = newShopItem.GetComponent<RectTransform>();
        
            //Configure the element according to the item
            newShopItem.Find("ItemName").GetComponent<TextMeshProUGUI>().SetText(type.ToString());
            newShopItem.Find("ItemPrize").GetComponent<TextMeshProUGUI>().SetText(ShopItem.GetCost(type).ToString());
        
            newShopItem.gameObject.SetActive(true);

            Button newItemBtn = newShopItem.GetComponent<Button>();
            if (newItemBtn)
            {
                newItemBtn.onClick.AddListener( () => buyEvent.Invoke(type));
            }
            else
            {
                Debug.Log("Button not found! ");
            }


        }

        private void Buy(ShopItem.ItemType item)
        {
            buyEvent.Invoke(item);
        }

        //Testing
    
        [ContextMenu("Create ItemGUI 1")]
        public void CreateItem1(){ CreateShopItem(ShopItem.ItemType.Item_1);}
    
        [ContextMenu("Create ItemGUI 2")]
        public void CreateItem2(){ CreateShopItem(ShopItem.ItemType.Item_2);}
    
        [ContextMenu("Create ItemGUI 3")]
        public void CreateItem3(){ CreateShopItem(ShopItem.ItemType.Item_3);}

        public void Configure(UIMediator mediator)
        {
            _mediator = mediator;
        }

        public void Show()
        {
            _canvasGroup.alpha = 1;
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        }
    
        public void Hide()
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }
    }
}
