using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance;

    public InventorySlotUI[] slots = new InventorySlotUI[8];

    private int nextSlot = 0;

    void Awake()
    {
        Instance = this;
    }

    public void AddItem(ItemData item)
    {
        if (item == null) return;

        if (nextSlot >= slots.Length)
            return; // full inventory (or handle overflow)

        slots[nextSlot].SetItem(item);
        nextSlot++;
    }
}