using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class UIInventoryPanel : MonoBehaviour
    {
        [SerializeField] private List<UIItemElement> _uiItems;

        public void ShowInventory(UIInventory uiInventory, InventoryBehaviour inventory)
        {
            for (var i = 0; i < _uiItems.Count; i++)
            {
                if (inventory.Items.Count > i)
                {
                    var index = i;
                    var itemId = inventory.Items[index].Id;
                    var sprite = InventoryItemScriptableObject.Instance.GetInventoryItem(itemId).Value.Sprite;

                    _uiItems[i].SetItemData(sprite);

                    _uiItems[i].AddListenerToButton(() =>
                    {
                        uiInventory.MoveObjcetFromInventory(inventory, index);
                    });
                }
                else
                {
                    _uiItems[i].SetItemData();

                    _uiItems[i].RemoveAllListenersFromButton();
                }
            }
        }
    }
}
