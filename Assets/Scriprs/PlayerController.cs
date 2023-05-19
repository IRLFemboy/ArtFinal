using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float horizontal;
    float vertical;
    public float speed = 8f;
    public float jumpForce = 16f;
    bool isFacingRight = true;
    bool isMoving;
    bool isFalling;
    public bool isJumping;

    [SerializeField] Rigidbody2D rb;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Animator anim;

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if(Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        Flip();

        if (rb.velocity.x > 0 || rb.velocity.x < 0)
        {
            isMoving = true;
            anim.SetBool("isMoving", isMoving);
        }
        else
        {
            isMoving = false;
            anim.SetBool("isMoving", isMoving);
        }

        if (rb.velocity.y > 0)
        {
            isJumping = true;
            anim.SetBool("isJumping", isJumping);
        }
        else
        {
            isJumping = false;
            anim.SetBool("isJumping", isJumping);
        }

        if (rb.velocity.y < 0)
        {
            isFalling = true;
            anim.SetBool("isFalling", isFalling);
        }
        else
        {
            isFalling = false;
            anim.SetBool("isFalling", isFalling);
        }

    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, 0);
    }

    bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, .2f, groundLayer);
    }

    void Flip()
    {
        if(isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Spike"))
        {
            Die();
        }
    }

    void Die()
    {

    }
}
