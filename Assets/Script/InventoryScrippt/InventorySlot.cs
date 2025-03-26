using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Item _carriedItem;

    private void Awake()
    {
        if (_carriedItem != null)
        {
            SpawnInventoryItem(_carriedItem);
        }
    }

    public void SpawnInventoryItem(Item _item)
    {
        GameObject newItem = Instantiate(InventoryManager.Singleton.itemPrefab, this.transform);
        newItem.GetComponent<Image>().sprite = _item.sprite;
    }
}
