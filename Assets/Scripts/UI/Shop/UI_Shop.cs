using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.Shop
{
    public class UIShop : MonoBehaviour
    {
        [SerializeField] private Transform container;
        [SerializeField] private GameObject shopItemTemplate;
        [SerializeField] private Button closeBtn;
        
        private CanvasGroup _canvasGroup;

    
        public UnityEvent<ShopItem.ItemType> buyEvent;

        private UIMediator _mediator;

        private void Awake()
        {
            _canvasGroup = gameObject.GetComponent<CanvasGroup>();
            container = _canvasGroup.gameObject.transform.Find("Container");
            closeBtn.onClick.AddListener(Hide);
            Hide();
        }

        private void CreateShopItem(ShopItem.ItemType type)
        {
            //Instantiate the new UI element
            Transform newShopItem = Instantiate(shopItemTemplate.transform, container);
            // RectTransform shopItemRT = newShopItem.GetComponent<RectTransform>();
        
            //Configure the element according to the item
            newShopItem.Find("ItemName").GetComponent<TextMeshProUGUI>().SetText(type.ToString());
            newShopItem.Find("ItemPrize").GetComponent<TextMeshProUGUI>().SetText(ShopItem.GetCost(type).ToString());
            newShopItem.Find("ItemSprite").GetComponent<Image>().sprite =
                ShopItem.GetSprite(type);
        
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
        
        //Testing

        [ContextMenu("Create items")]
        public void CreateItem1()
        {
            CreateShopItem(ShopItem.ItemType.Item1);
            CreateShopItem(ShopItem.ItemType.Item2);
            CreateShopItem(ShopItem.ItemType.Item3);
        }
    }
}
