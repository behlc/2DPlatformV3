using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{

    //public int health = 100;
    public int stars;
    public int playerMaxHealth;
    public int playerHealth;

    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    public float jumpForce = 10f;

    public Transform groundCheck;
    public float groundCheckradius = 0.2f;
    public LayerMask groundLayer;
    private bool isGrounded;
    private Animator animator;

    public int extraJumpsValue = 1; // one extra jump
    private int extraJumps; // actual amt of Jump left
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Slider playerHealthSlider;
    [SerializeField] private TMP_Text healthText;

    private AudioSource audioSource;
    public AudioClip jumpClip;
    public AudioClip hurtClip;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

        extraJumps = extraJumpsValue;
        playerHealth = playerMaxHealth;
        UpdateHealthSlider();
    }

    // Update is called once per frame
    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        if (moveInput >= 0)
        {
            spriteRenderer.flipX = false;

        } else spriteRenderer.flipX = true;
    
        if (isGrounded)
        {
            extraJumps = extraJumpsValue;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(isGrounded)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                PlaySFX(jumpClip);
            }
            else if(extraJumps > 0)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                extraJumps --;
                PlaySFX(jumpClip);
            }
    
        }

        SetAnimation(moveInput);
        
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckradius, groundLayer);
    }

    private void SetAnimation(float moveInput)
    {
        if(isGrounded)
        {
            if(moveInput == 0) // player not moving
            {
                animator.Play("Player_Idle");
            }
            else
            {
                animator.Play("Player_Run");

            }
        }
        else
        {
            if(rb.linearVelocityY > 0)
            {
                animator.Play("Player_Jump");
            }
            else
            {
                animator.Play("Player_Fall");
            }

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Damage")
        {
            PlaySFX(hurtClip);

            playerHealth -= 25;
            UpdateHealthSlider();

            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            StartCoroutine(BlinkRed());

            if(playerHealth <= 0)
            {
                Die();
            }
        }
        else if(collision.gameObject.tag == "BouncePad")
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce * 2);
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
        // this will reload the current active scene when player dies
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void UpdateHealthSlider()
    {
        playerHealthSlider.maxValue = playerMaxHealth;
        playerHealthSlider.value = playerHealth;
        healthText.text = playerHealthSlider.value + "/" + playerHealthSlider.maxValue;

    }

    public void PlaySFX(AudioClip audioClip, float volume = 0.2f)
    {
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.Play();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "PowerUp")
        {
            extraJumpsValue = 2;
            Destroy(collision.gameObject);
        }
    }
}
