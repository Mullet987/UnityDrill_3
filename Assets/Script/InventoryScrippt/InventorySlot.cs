using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private Item _carriedItem;

    private void Awake()
    {
        if (_carriedItem != null && this.transform.childCount == 0)
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
