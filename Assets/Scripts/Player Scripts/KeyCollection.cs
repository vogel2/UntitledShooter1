using UnityEngine;
using System.Collections.Generic;

public class KeyCollections : MonoBehaviour
{
    private Dictionary<string, int> keyTags = new Dictionary<string, int>()
    {
        { "Key 1", 1 },
        { "Key 2", 2 },
        { "Key 3", 3 }
    };
    private UIManager uiManager;

    private void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (keyTags.TryGetValue(other.tag, out int keyNumber))
        {
            other.gameObject.SetActive(false);
            uiManager.UpdateKeyUI(keyNumber);
            
           

        }
    }
}
