using MoreMountains.Feedbacks;
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
        [SerializeField] private MMF_Player shopMmfPlayer;

        
    
        public UnityEvent<GameItem> buyEvent;

        private UIMediator _mediator;

        private void Awake()
        {
            closeBtn.onClick.AddListener(Hide);
        }

        private void CreateShopItem(GameItem type)
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

        private void Buy(GameItem gameItem)
        {
            buyEvent.Invoke(gameItem);
        }

        public void Configure(UIMediator mediator)
        {
            _mediator = mediator;
        }

        public void Show()
        {
            shopMmfPlayer.PlayFeedbacks();
        }
    
        public void Hide()
        {
            shopMmfPlayer.PlayFeedbacks();
        }
        
        //Testing

        [ContextMenu("Create items")]
        public void CreateItem1()
        {
            CreateShopItem(GameItem.Item1);
            CreateShopItem(GameItem.Item2);
            CreateShopItem(GameItem.Item3);
        }
    }
}
