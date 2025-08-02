using UnityEngine;

public class KillzoneSpriteChange : MonoBehaviour
{
    public Sprite deadSprite;
    public Vector2 deadColliderOffset = new Vector2(0f, -0.3f);

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 1. Change to dead sprite directly
            SpriteRenderer sr = other.GetComponent<SpriteRenderer>();
            if (sr != null && deadSprite != null)
            {
                sr.sprite = deadSprite;
            }

            // 2. Adjust collider offset
            CapsuleCollider2D col = other.GetComponent<CapsuleCollider2D>();
            if (col != null)
            {
                col.offset = deadColliderOffset;
            }

            // 3. Stop movement
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = Vector2.zero;
            }

            // 4. Prevent control
            PlayerController controller = other.GetComponent<PlayerController>();
            if (controller != null)
            {
                controller.canMove = false;
            }

            // 5. Respawn
            PlayerRespawn respawn = other.GetComponent<PlayerRespawn>();
            if (respawn != null)
            {
                respawn.Respawn();
            }
        }
    }
}