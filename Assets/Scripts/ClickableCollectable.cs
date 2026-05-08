using UnityEngine;

public class ClickableCollectable : MonoBehaviour, IInteractable
{
    public ItemData item;

    public void OnInteract(GameObject interactor)
    {
        if (item == null) return;

        InventoryUI.Instance.AddItem(item);
        CollectableUI.Instance.ShowCollectable(item);

        Destroy(gameObject);
    }
}