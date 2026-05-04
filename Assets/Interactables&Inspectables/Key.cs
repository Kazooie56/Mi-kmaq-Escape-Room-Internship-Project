using UnityEngine;

public class Key : MonoBehaviour, IInteractable
{
    [SerializeField] private KeyData keyData;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnInteract(GameObject interactor)
    {
        if (keyData == null)
        {
            Debug.Log("no key data");
            return;
        }

        KeyInventory inventory = interactor.GetComponent<KeyInventory>();

        if (inventory != null)
        {
            inventory.AddKey(keyData);
            gameObject.SetActive(false);
        }
    }
}
