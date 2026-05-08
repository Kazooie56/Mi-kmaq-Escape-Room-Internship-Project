using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    public Image icon;

    void Awake()
    {
        if (icon != null)
            icon.enabled = false; // start hidden
    }

    public void SetItem(ItemData item)
    {
        if (icon == null) return;

        if (item == null)
        {
            icon.enabled = false;
            icon.sprite = null;
            return;
        }

        icon.sprite = item.icon;
        icon.enabled = true; // instantly visible
    }
}