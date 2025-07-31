using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpforce = 10f;
    public string horizontalInput = "Horizontal";
    public string jumpInput = "Jump";

    private Rigidbody2D rb;
    private bool isGrounded = false;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float move = Input.GetAxisRaw(horizontalInput);
        rb.velocity = new Vector2(move * speed, rb.velocity.y);

        if (Input.GetButtonDown(jumpInput) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpforce);
        }

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
    } 
}
