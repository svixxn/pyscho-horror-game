using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Button))]
    public class UIItemElement : MonoBehaviour
    {
        [SerializeField]
        private Image _itemImage;

        private Button _hideItemButton;
        private Button _itemButton
        {
            get
            {
                if (_hideItemButton == null)
                {
                    _hideItemButton = GetComponent<Button>();
                }

                return _hideItemButton;
            }
        }

        public void SetItemData(Sprite sprite = null)
        {
            _itemImage.gameObject.SetActive(sprite != null);
            _itemImage.sprite = sprite;
        }

        public void AddListenerToButton(UnityAction call)
        {
            _itemButton.onClick.RemoveAllListeners();
            _itemButton.onClick.AddListener(call);
        }

        public void RemoveAllListenersFromButton()
        {
            _itemButton.onClick.RemoveAllListeners();
        }
    }
}