using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerBetter : MonoBehaviour
{
    Rigidbody2D rb;
    public Healthbar healthbar;
    public Transform isGroundedChecker;
    public LayerMask groundLayer;
    public float speed;
    public float jumpForce;
    public float checkGroundRadius;
    public float rememberGroundedFor;
    public int defaultAdditionalJumps = 1;
    public int maxHealth = 10;
    public int CurrentHealth;
    public Animator animator;
    float lastTimeGrounded;
    float horizontalMove;
    bool isGrounded = false; 

    int additionalJumps;
     
     
    [Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        CheckIfGrounded();
        Flip();
        animationz();

        
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collisionGameObject = collision.gameObject;

        if (collisionGameObject.name == "Enemy")
        {
            TakeDamage(1);
        }
    }
    void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        healthbar.SetHealth(CurrentHealth);
        if(CurrentHealth <= 0)
        {
            Destroy(gameObject);
            
        }
    }
    void animationz()
    { 
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
    }
    void Move() 
    {  
        horizontalMove = Input.GetAxisRaw("Horizontal");
        float moveBy = horizontalMove * speed; 
        rb.velocity = new Vector2(moveBy, rb.velocity.y);
        
    }
    void Flip()
    {
        Vector3 characterScale = transform.localScale;
        if (Input.GetAxis("Horizontal") < 0)
        {
            characterScale.x = -0.2f;
        }
        if (Input.GetAxis("Horizontal") > 0)
        {
            characterScale.x = 0.2f;
        }
        transform.localScale = characterScale;
    }
    void Jump() 
    {
        if (Input.GetKeyDown(KeyCode.Space) && (isGrounded || Time.time - lastTimeGrounded <= rememberGroundedFor || additionalJumps > 0)) 
        {
            
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            additionalJumps--;
            
        }
    }
    void CheckIfGrounded() 
    {
        Collider2D colliders = Physics2D.OverlapCircle(isGroundedChecker.position, checkGroundRadius, groundLayer);
            if (colliders != null) 
            {
                isGrounded = true;
                animator.SetBool("IsJumping", false);
                additionalJumps = defaultAdditionalJumps;
            } 
        else 
        {
            if (isGrounded) 
            {   
                lastTimeGrounded = Time.time;
            }

            isGrounded = false;
            animator.SetBool("IsJumping", true);
            animator.SetTrigger("IsJumping");
        }
    }
}
