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

    public void RestoreOriginalSprite()
    {
        if (spriteRenderer != null && originalSprite != null)
            spriteRenderer.sprite = originalSprite;

        if (capsuleCollider != null)
            capsuleCollider.offset = originalColliderOffset;
    }
}
