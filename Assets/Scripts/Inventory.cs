//using System.Collections.Generic;
//using UnityEngine;

//public class Inventory : MonoBehaviour
//{
//    public static Inventory Instance;

//    public int maxSlots = 8;
//    public List<ItemData> items = new List<ItemData>();

//    void Awake()
//    {
//        Instance = this;
//    }

//    public bool AddItem(ItemData item)
//    {
//        if (items.Count >= maxSlots)
//        {
//            return false;
//        }

//        items.Add(item);
//        InventoryUI.Instance.Refresh(items);
//        return true;
//    }
//}