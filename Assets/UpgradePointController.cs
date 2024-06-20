using UnityEngine;
using UnityEngine.UI;

public class ChestController : MonoBehaviour
{
    public Animator chestAnimator;
    public int keysToCollect = 3;
    public Sprite depositedKeySprite;
    public Image[] keyImages; // Array of key UI Image components
    public float interactionDistance = 3f;

    private bool isOpen = false;
    private Transform playerTransform;

    void Start()
    {
        // Ensure the chest is initially in Idle state
        chestAnimator.SetTrigger("Idle");
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (Vector3.Distance(playerTransform.position, transform.position) <= interactionDistance && !isOpen)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                TryOpenChest();
            }
        }
    }

    void TryOpenChest()
    {
        int collectedKeys = 0;

        foreach (Image keyImage in keyImages)
        {
            if (keyImage.sprite != depositedKeySprite)
            {
                collectedKeys++;
                keyImage.sprite = depositedKeySprite;

                if (collectedKeys >= keysToCollect)
                {
                    OpenChest();
                    return;
                }
            }
        }
    }

    void OpenChest()
    {
        isOpen = true;
        chestAnimator.SetTrigger("OpenChestTrigger");
        Invoke("CloseChest", 5f); // Adjust the delay as needed
    }

    void CloseChest()
    {
        chestAnimator.SetTrigger("CloseChestTrigger");
        // Optionally, disable the collider to prevent further interaction
        GetComponent<Collider>().enabled = false;
    }
}
