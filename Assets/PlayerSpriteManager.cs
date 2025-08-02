using UnityEngine;

public class PlayerSpriteManager : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Sprite originalSprite;
    private CapsuleCollider2D playerCollider;
    private Vector2 originalColliderOffset;

    public Sprite deadSprite;
    public Vector2 deadColliderOffset = new Vector2(0f, -0.3f); // Adjust this as needed

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerCollider = GetComponent<CapsuleCollider2D>();

        if (spriteRenderer != null)
        {
            originalSprite = spriteRenderer.sprite;
        }

        if (playerCollider != null)
        {
            originalColliderOffset = playerCollider.offset;
        }
    }

    // Call this when player "dies"
    public void SetDeadSprite()
    {
        if (spriteRenderer != null && deadSprite != null)
        {
            spriteRenderer.sprite = deadSprite;
        }

        if (playerCollider != null)
        {
            playerCollider.offset = deadColliderOffset;
        }
    }

    // Call this on respawn to restore sprite and collider
    public void RestoreOriginalSprite()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = originalSprite;
        }

        if (playerCollider != null)
        {
            playerCollider.offset = originalColliderOffset;
        }
    }
}
