using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private bool isGrounded;
    public Transform GroundCheck;
    public LayerMask Ground;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckingGround();
        float dirX = Input.GetAxisRaw("Horizontal");
        rigidBody.velocity = new Vector2(dirX * 7f, rigidBody.velocity.y);
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector3(rigidBody.velocity.x, 5, 0);
            isGrounded = false;
        }

        Debug.Log(isGrounded);
    }

    void CheckingGround()
    {
        isGrounded = Physics2D.OverlapCircle(GroundCheck.position, 0.1f, Ground);
    }
}
