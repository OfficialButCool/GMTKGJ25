using UnityEngine;

public class TilemapTriggerDeactivator : MonoBehaviour
{
    [Header("Tilemaps to Deactivate")]
    public GameObject[] tilemapsToDeactivate;

    [Header("Tilemaps to Activate")]
    public GameObject[] tilemapsToActivate;

    [Header("UI Prompt (TextMeshPro Object)")]
    public GameObject pressEPromptUI;

    private bool playerInRange = false;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            // Deactivate all specified tilemaps
            foreach (GameObject go in tilemapsToDeactivate)
            {
                if (go != null)
                    go.SetActive(false);
            }

            // Activate all specified tilemaps
            foreach (GameObject go in tilemapsToActivate)
            {
                if (go != null)
                    go.SetActive(true);
            }

            // Hide the "Press E" prompt
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
