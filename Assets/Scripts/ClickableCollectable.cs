using UnityEngine;

public class ClickableCollectable : MonoBehaviour
{
    public ItemData item;

    private void OnMouseDown()
    {
        if (item == null) return;

        InventoryUI.Instance.AddItem(item);
        CollectableUI.Instance.ShowItem(item);

        Destroy(gameObject);
    }
}