using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 20f;

    public Transform groundCheck;
    public float groundCheckRadius = 0.3f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool isGrounded;
    private Vector2 moveInput;
    private bool jumpPressed;

    public bool canMove = true;

    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction jumpAction;

    private bool isDead = false; // 🔹 New flag

    // --- New sprite fields ---
    public Sprite idleRightSprite;
    public Sprite idleLeftSprite;
    public Sprite jumpRightSprite;
    public Sprite jumpLeftSprite;

    private SpriteRenderer spriteRenderer;
    private bool facingRight = true;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        if (playerInput == null)
        {
            Debug.LogError("PlayerInput component missing!");
            return;
        }

        moveAction = playerInput.actions["Move"];
        if (moveAction == null)
        {
            Debug.LogError("Move action not found in Input Actions!");
        }

        jumpAction = playerInput.actions["Jump"];
        if (jumpAction == null)
        {
            Debug.LogError("Jump action not found in Input Actions!");
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!canMove) return; // skip movement

        moveInput = moveAction.ReadValue<Vector2>();
        jumpPressed = jumpAction.WasPressedThisFrame();

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (jumpPressed && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        rb.velocity = new Vector2(moveInput.x * speed, rb.velocity.y);

        // Update facing direction based on input
        if (moveInput.x > 0) facingRight = true;
        else if (moveInput.x < 0) facingRight = false;

        // Change sprite based on grounded state and facing direction
        if (!isGrounded)
        {
            spriteRenderer.sprite = facingRight ? jumpRightSprite : jumpLeftSprite;
        }
        else
        {
            spriteRenderer.sprite = facingRight ? idleRightSprite : idleLeftSprite;
        }
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }

    // 🔹 Call when player dies
    public void DisableMovement()
    {
        isDead = true;
        rb.velocity = Vector2.zero;
    }

    // 🔹 Call when player respawns
    public void EnableMovement()
    {
        isDead = false;
    }
}
