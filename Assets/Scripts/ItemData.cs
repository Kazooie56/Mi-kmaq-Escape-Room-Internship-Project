using UnityEngine;

// this lets you create things in the project tab
[CreateAssetMenu(menuName = "Inventory/Item")]
public class ItemData : ScriptableObject
{
    public string displayName;
    public Sprite icon;
    public string description;
}