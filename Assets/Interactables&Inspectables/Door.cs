using UnityEngine;

public class Door : MonoBehaviour,IInteractable
{
    [SerializeField] private KeyData requiredKey;

    public void OnInteract(GameObject interactor)
    {
        if (requiredKey == null)
        {
            Debug.Log("no key data");
            return;
        }

        KeyInventory inventory = interactor.GetComponent<KeyInventory>();

        if (inventory == null) return;

        if (inventory.HasKey(requiredKey))
        {
            gameObject.SetActive(false);
            Debug.Log("Open door");
        }
        else
        {
            Debug.Log("The door is locked");
        }
    }
}
