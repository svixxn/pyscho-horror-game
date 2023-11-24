using UnityEngine;

namespace UI
{
	public class UIInventory : MonoBehaviour
	{
		[SerializeField]
		private UIInventoryPanel _playerInventoryPanel;

		[SerializeField]
		private UIInventoryPanel _otherInventoryPanel;

		private InventoryBehaviour _playerInventory;
		private InventoryBehaviour _otherInventory;

		public void ShowInventory(InventoryBehaviour playerInventory, InventoryBehaviour otherInventory = null)
		{
			_playerInventory = playerInventory;
			_otherInventory = otherInventory;

			if (otherInventory == null)
			{
				_otherInventoryPanel.gameObject.SetActive(false);
			}
			else
			{
				_otherInventoryPanel.gameObject.SetActive(true);
				_otherInventoryPanel.ShowInventory(this, otherInventory);
			}

			_playerInventoryPanel.ShowInventory(this, playerInventory);
		}

		public void MoveObjcetFromInventory(InventoryBehaviour fromInventory, int index)
		{
			var toInventory = _playerInventory;
			if (toInventory == fromInventory)
			{
				toInventory = _otherInventory;
			}

			if (toInventory == null)
			{
				return;
			}

			var item = fromInventory.Items[index];
			toInventory.AddItem(item.Id, item.Count);
			fromInventory.RemoveItemAtIndex(index);

			ShowInventory(_playerInventory, _otherInventory);
		}
	}
}
