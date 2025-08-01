using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public float fallThreshold = 10f; // How far below checkpoint before respawn
    private Vector2 checkpoint;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        checkpoint = transform.position; // Start point
    }

    void Update()
    {
        if (transform.position.y < checkpoint.y - fallThreshold)
        {
            Respawn();
        }
    }

    public void SetCheckpoint(Vector2 newCheckpoint)
    {
        checkpoint = newCheckpoint;
    }

    public void Respawn()
    {
        rb.velocity = Vector2.zero; // Reset motion
        transform.position = checkpoint;
    }
}
