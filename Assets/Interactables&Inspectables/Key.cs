using UnityEngine;

public class Key : MonoBehaviour, IInteractable
{
    [SerializeField] private KeyData keyData;

    public void OnInteract(GameObject interactor)
    {
        if (keyData == null) { Debug.Log("no key data"); return; }

        KeyInventory inventory = interactor.GetComponent<KeyInventory>();

        if (inventory != null)
        {
            inventory.AddKey(keyData);

            InventoryUI.Instance.AddItem(keyData);
            CollectableUI.Instance.ShowCollectable(keyData);

            gameObject.SetActive(false);
        }
    }
}
