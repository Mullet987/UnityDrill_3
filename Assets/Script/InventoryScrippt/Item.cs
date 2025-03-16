using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Item")]

public class Item : ScriptableObject
{
    public Sprite sprite;
    public int itemIndex;
    public string itemName;
    public string itemDescription;
    //public stirng[] itemProperties;
}
