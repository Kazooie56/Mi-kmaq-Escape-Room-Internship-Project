using UnityEngine;

// this lets you create things in the project tab
[CreateAssetMenu(menuName = "Inventory/Item")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public string collectDescription;
}
