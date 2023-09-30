using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLiikumine : MonoBehaviour
{
    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 18f;
    private bool isFacingRight = true;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private Animator anim;  // Uus muutuja animatsiooni jaoks

    void Start()
    {
        anim = GetComponent<Animator>();  // Saame Animator komponendi
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");


        // Uus: Animatsiooni loogika
        if (horizontal != 0)
        {
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }

        if (vertical != 0 && !IsGrounded())
        {
            anim.SetBool("Jump", true);
            anim.SetBool("isWalking", false);
        }
        else
        {
            anim.SetBool("Jump", false);
        }

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        Flip();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
