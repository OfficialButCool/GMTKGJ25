using UnityEngine;

public class KillzoneSpriteChange : MonoBehaviour
{
    public Sprite deadSprite;                // Assign the "dead" sprite in Inspector
    public Vector2 deadColliderOffset = new Vector2(0f, -0.3f);  // Adjust as needed

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Change the player's sprite to the dead sprite
            SpriteRenderer playerSpriteRenderer = other.GetComponent<SpriteRenderer>();
            if (playerSpriteRenderer != null && deadSprite != null)
            {
                playerSpriteRenderer.sprite = deadSprite;
            }

            // Adjust the player's collider offset if using CapsuleCollider2D
            CapsuleCollider2D playerCollider = other.GetComponent<CapsuleCollider2D>();
            if (playerCollider != null)
            {
                playerCollider.offset = deadColliderOffset;
            }

            // Disable movement
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = Vector2.zero;  // Stop sliding
            }

             PlayerController controller = other.GetComponent<PlayerController>();
            if (controller != null)
            {
                controller.canMove = false; // Prevent further movement
            }

            // Trigger respawn with black screen delay
            PlayerRespawn respawn = other.GetComponent<PlayerRespawn>();
            if (respawn != null)
            {
                respawn.Respawn();
            }
        }
    }
}
