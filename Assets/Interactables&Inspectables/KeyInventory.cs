using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class KeyInventory : MonoBehaviour
{
    private List<KeyData> keys = new List<KeyData>();

    public void AddKey(KeyData keyFound)
    {
        if (!keys.Contains(keyFound))
        {
            keys.Add(keyFound);
        }
    }

    public bool HasKey(KeyData requiredKey)
    {
        // Checks any key for the Keyid, will reutrn true if found or false if not
        return keys.Any(key => key.keyID == requiredKey.keyID);
    }

    public void RemoveKey(KeyData keyToRemove)
    {
        keys.Remove(keyToRemove);
    }
}