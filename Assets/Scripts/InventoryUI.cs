using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance;

    public InventorySlotUI[] slots = new InventorySlotUI[8];

    private List<ItemData> items = new List<ItemData>();

    void Awake()
    {
        Instance = this;
    }

    public void AddItem(ItemData item)
    {
        if (item == null) return;

        if (items.Count >= slots.Length)
            return;

        items.Add(item);
        RefreshUI();
    }

    public void RemoveItem(ItemData item)
    {
        if (item == null) return;

        items.Remove(item);

        RefreshUI();
    }

    private void RefreshUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < items.Count)
            {
                slots[i].SetItem(items[i]);
            }
            else
            {
                slots[i].SetItem(null);
            }
        }
    }
}