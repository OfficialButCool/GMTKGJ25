using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Vector2 checkpoint;
    private Rigidbody2D rb;
    private AudioSource audioSource;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        checkpoint = transform.position; // Starting checkpoint
        Debug.Log("Initial checkpoint set to: " + checkpoint);
    }

    public void SetCheckpoint(Vector2 newCheckpoint)
    {
        checkpoint = newCheckpoint;
        Debug.Log("Checkpoint updated to: " + checkpoint);
    }

    public void Respawn()
    {
        if (audioSource != null)
        {
            audioSource.PlayOneShot(audioSource.clip);

        }
        rb.velocity = Vector2.zero;
        transform.position = checkpoint;
        Debug.Log("Respawned at: " + checkpoint);
    }
}
