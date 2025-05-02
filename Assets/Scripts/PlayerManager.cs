using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    Rigidbody2D rb => GetComponent<Rigidbody2D>();
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private SpriteRenderer outline;
    private float horizontal;
    private bool isFacingRight = true;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float moveSpeed;
    private bool isMoving;
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private LayerMask groundLayer;
    private bool isGrounded;
    public float coyoteTime = 10f;
    public float coyoteTimeCounter;
    [SerializeField]
    private Transform wallCheck;
    [SerializeField]
    private LayerMask wallLayer;
    public float slideSpeed = 2f;
    public bool isWallSliding;
    private bool isWallJumping;
    private float wallJumpingDirection;
    private float wallJumpingTime = 0.2f;
    private float wallJumpingCounter;
    private float wallJumpingDuration = 0.35f;
    private Vector2 wallJumpingPower = new Vector2(7f, 10f);
    public bool canDash = true;
    public bool isDashing;
    public bool hasJump;
    private float dashForce = 24f;
    private float dashTime = 0.15f;
    private float dashCD = 0.2f;
    private SpriteRenderer sprite;
    [SerializeField]
    private GameObject particles;
    public bool isWallBoucning;


    // Start is called before the first frame update
    void Start()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>();
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {

        horizontal = Input.GetAxisRaw("Horizontal");

        if(IsGrounded())
        {
            coyoteTimeCounter = coyoteTime;
            canDash = true;
            sprite.color = new Color32(255, 255, 255, 255);
            hasJump = true;
        }
        else if (IsWalled())
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (isDashing)
        {
            return;
        }

        if(isWallBoucning)
        {
            return;
        }

        if(Time.timeScale != 1)
        {
            return ;
        }


        if (!sprite.enabled)
        {
            return;
        }

        if (!isWallJumping && sprite.enabled == true)
        {
            Movement();
        } 

        

        if (!isFacingRight &&Input.GetKeyDown(KeyCode.RightShift) && canDash || !isFacingRight && Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(LeftDash());
            
        }

        if (isFacingRight && Input.GetKeyDown(KeyCode.RightShift) && canDash || isFacingRight && Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(RightDash());
        }
        else if (Input.GetKeyDown(KeyCode.RightShift) && canDash)
        {
            StartCoroutine(RightDash());
        }

        if (!isWallJumping)
        {
            Flip();
        }
        WallSlide();
        WallJump();
        Outline();
        
        

    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    public bool IsWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }

    private void WallSlide()
    {
        if(IsWalled() && horizontal !=0f)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -slideSpeed, float.MaxValue));
            sprite.color = new Color32(255, 255, 255, 255);
        }
        else
        {
            isWallSliding = false;
        }

        if(isWallSliding && rb.velocity.y !=0)
        {
            particles.SetActive(true);
        }
        else
        {
            particles.SetActive(false);
        }
    }

    private void WallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;
            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingCounter -=Time.deltaTime;
        }

        if(Input.GetKeyDown(KeyCode.Space) && wallJumpingCounter > 0f && !IsGrounded())
        {
            isWallJumping = true;
            hasJump = false;
            rb.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;
            sprite.color = new Color32(128, 128, 128, 255);

            if (transform.localScale.x != wallJumpingDirection)
            {
                isFacingRight = !isFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }

            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }

        
    }

    private void StopWallJumping()
    {
        isWallJumping = false;
    }

    private void Flip()
    {
        if(isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void Jump()
    {
        if (coyoteTimeCounter > 0 && hasJump)
        {
            float currentVelocity = rb.velocity.x;
            rb.velocity = new Vector2 (currentVelocity, 0);
            rb.velocity += Vector2.up * jumpForce;
            isGrounded = false;
            coyoteTimeCounter = 0f;
            hasJump = false;
            Invoke(nameof(JumpColor), 0.1f );
            
        }
    }

    private void JumpColor()
    {
        sprite.color = new Color32(128, 128, 128, 255);
    }


    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("SwitchBlock"))
        {
            isGrounded = true;
            rb.velocity = Vector2.zero;
        }

        if (collision.gameObject.CompareTag("Lava"))
        {
            //rb.transform.position = new Vector2(-7, -1.95f);

        
        }

    }

    private void Outline()
    {
        if(canDash)
        {
            outline.enabled = true;
        }
        else if(!canDash)
        {
            outline.enabled=false;
        }
  
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Victory"))
        {
            PlayerPrefs.SetInt("Level1", 1);
            SceneManager.LoadScene("Level Select");
            
        }
        if (collision.gameObject.CompareTag("Victory2"))
        {
            PlayerPrefs.SetInt("Level2", 1);
            SceneManager.LoadScene("Level Select");

        }
        if (collision.gameObject.CompareTag("Victory3"))
        {
            PlayerPrefs.SetInt("Level3", 1);
            SceneManager.LoadScene("Level Select");

        }
        if (collision.gameObject.CompareTag("Victory4"))
        {
            PlayerPrefs.SetInt("Level4", 1);
            SceneManager.LoadScene("Level Select");

        }

    }

    private void Movement()
    {
        if (Input.GetKey(KeyCode.A))
        {
            isMoving = true;
            rb.velocity = new Vector2(-1 * moveSpeed , rb.velocity.y);
        }

        if(Input.GetKeyUp(KeyCode.A))
        {
            isMoving = false;
            if (isGrounded)
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
            else if (!isGrounded)
            {
                float targetX = 0.1f * rb.velocity.x;
                float newX = Mathf.Lerp(rb.velocity.x, targetX, 0.5f);
                rb.velocity = new Vector2(newX, rb.velocity.y);
            }
          
        }

        if (Input.GetKey(KeyCode.D))
        {
            isMoving = true;
            rb.velocity = new Vector2(1 * moveSpeed , rb.velocity.y);
       
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            isMoving = false;
            if (isGrounded)
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
            else if (!isGrounded)
            {
                float targetX = 0.1f * rb.velocity.x;
                float newX = Mathf.Lerp(rb.velocity.x, targetX, 0.5f);
                rb.velocity = new Vector2(newX, rb.velocity.y);
                
            }
           
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
            
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            coyoteTimeCounter = 0f;
        }
    }

    private IEnumerator LeftDash()
    {
            canDash = false;
            isDashing = true;
            float originalGravity = rb.gravityScale;
            rb.gravityScale = 0;
            rb.velocity = new Vector2(-1 * dashForce, 0f);
            yield return new WaitForSeconds(dashTime);
            rb.gravityScale= originalGravity;
            isDashing = false;
            if(isWallBoucning)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
        }
            else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
            yield return new WaitForSeconds(dashCD);
        /*if (!isMoving)
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }*/
           
        
    }

    private IEnumerator RightDash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0;
        rb.velocity = new Vector2(1 * dashForce, 0f);
        yield return new WaitForSeconds(dashTime);
        rb.gravityScale = originalGravity;
        isDashing = false;
        if (isWallBoucning)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        yield return new WaitForSeconds(dashCD);
    }


 





}
