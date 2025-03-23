using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private Item _carriedItem;

    private void Awake()
    {
        if (_carriedItem != null)
        {
            SpawnInventoryItem(_carriedItem);
        }
    }

    public void SpawnInventoryItem(Item _item)
    {
        if (InventoryManager.Singleton == null)
        {
            Debug.LogError("SpawnInventoryItem: InventoryManager.Singleton is NULL!");
            return;
        }

        if (InventoryManager.Singleton.itemPrefab == null)
        {
            Debug.LogError("SpawnInventoryItem: itemPrefab is NULL in InventoryManager!");
            return;
        }

        if (_item == null)
        {
            Debug.LogError("SpawnInventoryItem: _item is NULL!");
            return;
        }

        if (_item.sprite == null)
        {
            Debug.LogError("SpawnInventoryItem: _item.sprite is NULL!");
            return;
        }
        //GameObject newItem = Instantiate(InventoryManager.Singleton.itemPrefab, this.transform);
        GameObject newItem = Instantiate(InventoryManager.Singleton.itemPrefab, this.transform);
        newItem.GetComponent<Image>().sprite = _item.sprite;
    }
}
