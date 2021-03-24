using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Physics
    [SerializeField] float normalGravity =3;
    [SerializeField] float fallGravity =4;
    [SerializeField] float floatGravity =0.5f;
    Rigidbody2D rigid;
    Animator animator;

    //Jump
    [SerializeField] float groundTime = 0.2f;
    [SerializeField] float jumpTime = 0.2f;
    float groundTimer = 0f;
    float jumpTimer = 0f;
    bool booJump;

    //Collision
    [SerializeField]
    LayerMask collisionLayer;

    //Movement
    [SerializeField] float speedY = 10f;
    [SerializeField] float speedX = 10f;
    float movementSpeed;

    [SerializeField] float gcPosY = -0.01f;
    [SerializeField] float gcScale = -0.02f;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        MovementInput();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void MovementInput() 
    {
        //Check if player hit the ground
        Vector2 groundCheckPos = (Vector2)transform.position + new Vector2(0, gcPosY);
        Vector2 groundCheckScale = (Vector2)transform.localScale + new Vector2(gcScale, 0);
        bool booGround = Physics2D.OverlapBox(groundCheckPos, groundCheckScale, 0, collisionLayer);
        //Tick down the timers
        groundTimer -= Time.deltaTime;
        jumpTimer -= Time.deltaTime;

        //jump timer and coyote time activated for smoother gameplay
        if (Input.GetButtonDown("Jump")) { jumpTimer = jumpTime; }
        if (booGround) {groundTimer = groundTime; }

        //If jump was pressed, and player touched the ground, jump
        if ((jumpTimer > 0) && (groundTimer > 0))
        {
            groundTimer = 0;
            jumpTimer = 0;
            booJump = true;
        }
        movementSpeed = Input.GetAxisRaw("Horizontal") * speedX;

        if (rigid.velocity.y >= 0)
        {
            rigid.gravityScale = normalGravity;
        }
        else
        {
            rigid.gravityScale = fallGravity;
        }
        if (Input.GetButton("Jump"))
        {
            
        }

        if (booGround)
        {
            if (Input.GetAxisRaw("Horizontal") != 0)
            {
                animator.SetTrigger("Walk");
            }
            else
            {
                animator.SetTrigger("Stop");
            }
            if (Input.GetButtonDown("Fire1"))
            {
                animator.SetTrigger("Duck");
            }
            if (Input.GetButtonUp("Fire1"))
            {
                animator.SetTrigger("DuckUp");
            }
        }
        else 
        {

        }
        if (Input.GetAxisRaw("Horizontal") > 0) 
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void Move()
    {
        if (booJump)
        {
            booJump = false;
            rigid.velocity = new Vector2(rigid.velocity.x, speedY);
        }
        rigid.velocity = new Vector2(movementSpeed, rigid.velocity.y);
    }
}
