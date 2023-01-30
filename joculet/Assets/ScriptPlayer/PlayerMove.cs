using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    public float speed;
    [SerializeField]
    public float jumpPower;
    [SerializeField]
    private float jumpforce;
    public float Move;
    [SerializeField]
    Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask groundLayer;
     [SerializeField] private LayerMask wallLayer;
    private float wallJumpCooldown;

    private bool facingRight;
    public bool onGround;
    public bool onWALL;
     public bool isRunning = false;
    public Animator anim;
    public float jumpForce = 10f;
    public Text WINTEXT;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
    void Update()
    {
        
        Move = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(speed * Move, rb.velocity.y);

        if (rb == null)
        {
            Debug.LogError("Rigidbody2D is not assigned!");
        }
        if (facingRight == false && Move < 0)
        {
            Flip();
        }
        else if (facingRight == true && Move > 0)
        {
            Flip();
        }
        anim.SetBool("Run", Move!=0);
        anim.SetBool("Grounded", isGrounded());
       
            
        if(wallJumpCooldown > 0.2f)
        {
            
            rb.velocity = new Vector2(speed * Move, rb.velocity.y);

            if (onWall() && !isGrounded())
            {
                rb.gravityScale = 0;
                rb.velocity = Vector2.zero;
                anim.SetBool("OnWALL", Move == 0);
            }
            else
                rb.gravityScale = 1.5F;
            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }
        }
        else
        {
            wallJumpCooldown += Time.deltaTime;
        }

    }
    
    private void Jump()
    {
        if(isGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            anim.SetTrigger("Jump");
        }
        else if(onWall() && !isGrounded())
        {

            rb.velocity = new Vector2(-jumpForce, jumpForce);
        }
        wallJumpCooldown = 0;

    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
   public void OnCollisionEnter2D(Collision2D colision)
    {
        if (colision.gameObject.CompareTag("Ground"))
        {
            onGround = true;
        }
    }
    public void OnCollisionExit2D(Collision2D colision)
    {
        if (colision.gameObject.CompareTag("Ground"))
        {
            onGround = false;
            
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Win")
        {
            WINTEXT.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }
    public bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x,0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }
    public bool canAttack()
    {
        return Move == 0 && isGrounded() && !onWall();
    }

}
