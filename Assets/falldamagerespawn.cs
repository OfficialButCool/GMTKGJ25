using UnityEngine;
using System.Collections;

public class PlayerRespawn : MonoBehaviour
{
    private Vector2 checkpoint;
    private Rigidbody2D rb;
    private AudioSource audioSource;
    private PlayerSpriteManager spriteManager;
    private PlayerController playerController;

    [Header("Fade Settings")]
    public GameObject blackScreenUI;  // Assign your black screen panel here
    public float fadeDuration = 1f;   // Duration of fade in/out
    public float fadeDelay = 0.5f;    // Delay while fully black

    private CanvasGroup blackScreenCanvasGroup;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        spriteManager = GetComponent<PlayerSpriteManager>();
        playerController = GetComponent<PlayerController>(); // <-- Get player controller

        checkpoint = transform.position; // Starting checkpoint
        Debug.Log("Initial checkpoint set to: " + checkpoint);

        if (blackScreenUI != null)
        {
            blackScreenCanvasGroup = blackScreenUI.GetComponent<CanvasGroup>();
            blackScreenUI.SetActive(false);
            if (blackScreenCanvasGroup != null)
                blackScreenCanvasGroup.alpha = 0f;
        }
    }

    public void SetCheckpoint(Vector2 newCheckpoint)
    {
        checkpoint = newCheckpoint;
        Debug.Log("Checkpoint updated to: " + checkpoint);
    }

    public void Respawn()
    {
        // Start the fade and respawn coroutine
        StartCoroutine(FadeRespawnCoroutine());
    }

    private IEnumerator FadeRespawnCoroutine()
    {
        if (blackScreenUI != null && blackScreenCanvasGroup != null)
        {
            blackScreenUI.SetActive(true);

            // Play audio right as black screen starts fading in
            if (audioSource != null)
            {
                audioSource.PlayOneShot(audioSource.clip);
            }

            // Fade to black
            float time = 0f;
            while (time < fadeDuration)
            {
                blackScreenCanvasGroup.alpha = Mathf.Lerp(0f, 1f, time / fadeDuration);
                time += Time.deltaTime;
                yield return null;
            }
            blackScreenCanvasGroup.alpha = 1f;

            // Wait fully black
            yield return new WaitForSeconds(fadeDelay);
        }

        // Teleport player to checkpoint and reset velocity
        rb.velocity = Vector2.zero;
        transform.position = checkpoint;
        Debug.Log("Respawned at: " + checkpoint);

        // Restore original sprite ONLY on respawn
        if (spriteManager != null)
        {
            spriteManager.RestoreOriginalSprite();
            spriteManager.UpdateOriginalState(); // Update baseline to current sprite after respawn
        }

        // Re-enable movement after respawn
        if (playerController != null)
        {
            playerController.canMove = true;
        }

        // Re-enable physics on player Rigidbody
        if (rb != null)
        {
            rb.isKinematic = false;
        }

        if (blackScreenUI != null && blackScreenCanvasGroup != null)
        {
            // Fade back to transparent
            float time = 0f;
            while (time < fadeDuration)
            {
                blackScreenCanvasGroup.alpha = Mathf.Lerp(1f, 0f, time / fadeDuration);
                time += Time.deltaTime;
                yield return null;
            }
            blackScreenCanvasGroup.alpha = 0f;
            blackScreenUI.SetActive(false);
        }
    }
}

