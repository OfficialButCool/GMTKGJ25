using UnityEngine;

public class PlayerSpriteManager : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private CapsuleCollider2D capsuleCollider;

    private Sprite originalSprite;
    private Vector2 originalColliderOffset;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();

        if (spriteRenderer != null)
            originalSprite = spriteRenderer.sprite;

        if (capsuleCollider != null)
            originalColliderOffset = capsuleCollider.offset;
    }

    // Call this to set the dead sprite and offset
    public void SetDeadSprite(Sprite deadSprite, Vector2 deadOffset)
    {
        if (spriteRenderer != null && deadSprite != null)
            spriteRenderer.sprite = deadSprite;

        if (capsuleCollider != null)
            capsuleCollider.offset = deadOffset;
    }

    // Call this to restore original sprite and collider offset
    public void RestoreOriginalSprite()
    {
        if (spriteRenderer != null && originalSprite != null)
            spriteRenderer.sprite = originalSprite;

        if (capsuleCollider != null)
            capsuleCollider.offset = originalColliderOffset;
    }

    // Call this when respawning to update the original sprite and collider offset
    public void UpdateOriginalState()
    {
        if (spriteRenderer != null)
            originalSprite = spriteRenderer.sprite;

        if (capsuleCollider != null)
            originalColliderOffset = capsuleCollider.offset;
    }
}
