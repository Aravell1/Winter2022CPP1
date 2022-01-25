using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]
public class Player : MonoBehaviour
{
    public bool verbose = false;
    public bool isGrounded;

       
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;

    [SerializeField]
    float speed;

    [SerializeField]
    int jumpForce;

    [SerializeField]
    float groundCheckRadius;

    [SerializeField]
    LayerMask isGroundLayer;

    [SerializeField]
    Transform groundCheck;




    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        if (speed <= 0)
        {
            speed = 3.0f;
            if (verbose)
            {
                Debug.Log("Speed changed to default value of 3");
            }
        }

        if (jumpForce <= 0)
        {
            jumpForce = 300;
            if (verbose)
            {
                Debug.Log("Jump Force changed to default value of 300");
            }
        }

        if (groundCheckRadius <= 0)
        {
            groundCheckRadius = 0.05f;
            if (verbose)
            {
                Debug.Log("Ground Check Layer changed to default value of 0.05");
            }
        }

        if (!groundCheck)
        {
            groundCheck = transform.GetChild(0);
            if (verbose)
            {
                if (groundCheck.name == "GroundCheck")
                {
                    Debug.Log("Ground Check Found and Assigned");
                }
                else
                {
                    Debug.Log("Manually set ground check as it could not be found!");
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        float hInput = Input.GetAxisRaw("Horizontal");

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * jumpForce);
        }

        Vector2 moveDir = new Vector2(hInput * speed, rb.velocity.y);
        rb.velocity = moveDir;

        anim.SetFloat("xVel", Mathf.Abs(hInput));
        anim.SetBool("isGrounded", isGrounded);

        if (Input.GetButtonDown("Fire1"))
        {
            anim.SetBool("attack", true);
        }
        if (Input.GetButtonUp("Fire1"))
        {
            anim.SetBool("attack", false);
        }


        if (Input.GetKeyDown(KeyCode.W))
        {
            anim.SetBool("float", true);
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            anim.SetBool("float", false);
        }

        if (hInput < 0)
        {
            sr.flipX = true;
        }
        if (hInput > 0)
        {
            sr.flipX = false;
        }
    }
}
