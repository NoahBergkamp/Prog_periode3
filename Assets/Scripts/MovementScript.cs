using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MovementScript : MonoBehaviour
{
    public int coins = 0; 
    public int health = 100;
    public float MovementSpeed = 5f;
    public float jumpForce = 10f;
    public Transform GroundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool isGrounded;

    private Animator animator;

    private SpriteRenderer spriteRenderer;

    public int extraJumpValue = 1;
    public int extraJumps;
    public Image HealthImage;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        extraJumps = extraJumpValue;
    }


    
    void Update()
    {
        float MoveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(MoveInput * MovementSpeed, rb.linearVelocity.y);

        if (isGrounded)
        {
            extraJumps = extraJumpValue;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            }
            else if (extraJumps > 0) 
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                extraJumps--;
            }

        }
        if(rb.linearVelocityX != 0)
        {
            spriteRenderer.flipX = (rb.linearVelocityX) < 0f;
        }
        
        setAnimation(MoveInput);

        HealthImage.fillAmount = health/100f;
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(GroundCheck.position, groundCheckRadius, groundLayer);
    }

    private void setAnimation(float MoveInput)
    {
        if (isGrounded)
        {
            if (MoveInput == 0)
            {
                animator.Play("IdlePlayerAnimation");
            }
            else
            {
                animator.Play("PlayerRunAnimation");
            }
        }

        else
        {
            if (rb.linearVelocityY > 0)
            {
                animator.Play("PlayerJumpAnimation");

            }
            else 
            {
                animator.Play("PlayerFallAnimation");

            }
        }

        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Damage")
        {
            health -= 25;

            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            StartCoroutine(BlinkRed());

            if (health <= 0)
            {
                Die();
            }
        }

        
    }

    private IEnumerator BlinkRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;

    }

    private void Die()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
    }
}
