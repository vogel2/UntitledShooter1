using UnityEngine;

public class Key : MonoBehaviour
{
    // This method is called when another collider enters the trigger collider attached to the Key
    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider belongs to the player
        if (other.CompareTag("Player"))
        {
            // Perform your desired action here (e.g., increase key count, update UI, etc.)
            Debug.Log("Key collected by player: " + gameObject.name);

            // Deactivate the key to simulate it being picked up
            gameObject.SetActive(false);
        }
    }
}