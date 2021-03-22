using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Physics
    [SerializeField] float normalGravity;
    [SerializeField] float fallGravity;
    [SerializeField] float floatGravity;
    Rigidbody2D rigid;

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

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
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
        Vector2 groundCheckPos = (Vector2)transform.position + new Vector2(0, -0.01f);
        Vector2 groundCheckScale = (Vector2)transform.localScale + new Vector2(-0.02f, 0);
        bool booGround = Physics2D.OverlapBox(groundCheckPos, groundCheckScale, 0, collisionLayer);

        //Tick down the timers
        groundTimer -= Time.deltaTime;
        jumpTimer -= Time.deltaTime;

        //jump timer and coyote time activated for smoother gameplay
        if (Input.GetButtonDown("Jump")) { jumpTimer = jumpTime; }
        if (booGround) { groundTimer = groundTime; }

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
            Debug.Log("It keeps happening while I press the button");
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
