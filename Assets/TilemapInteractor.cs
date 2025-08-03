using UnityEngine;

public class TilemapTriggerActivator : MonoBehaviour
{
    [Header("Tilemap GameObject to Activate")]
    public GameObject tilemapToActivate;

    [Header("UI Prompt (TextMeshPro Object)")]
    public GameObject pressEPromptUI;  // Assign the "Press E" TextMeshPro object in the inspector

    private bool playerInRange = false;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (tilemapToActivate != null)
                tilemapToActivate.SetActive(true);

            if (pressEPromptUI != null)
                pressEPromptUI.SetActive(false);

            playerInRange = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;

            if (pressEPromptUI != null)
                pressEPromptUI.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;

            if (pressEPromptUI != null)
                pressEPromptUI.SetActive(false);
        }
    }
}
