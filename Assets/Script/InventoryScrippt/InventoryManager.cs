using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Singleton;
    public GameObject itemPrefab;

    [SerializeField] private Transform _draggablesTransform;

    private void Awake()
    {
        Singleton = this;
    }

    public void SetCarriedItem(InventorySlot itemSlot)
    {
        if (itemSlot.transform.childCount != 0)
        {
            Transform itemObject = itemSlot.transform.GetChild(0);
            itemObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
            itemObject.transform.SetParent(_draggablesTransform);
        }
    }

}
