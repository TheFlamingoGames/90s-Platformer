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
    [SerializeField] float jumpTriggerTime = 0.2f;
    [SerializeField] float jumpTime = 0.5f;
    float groundTimer = 0f;
    float jumpTriggerTimer = 0f;
    float jumpTimer = 0f;
    bool booJump;

    //Collision
    [SerializeField]
    LayerMask collisionLayer;

    //Movement
    [SerializeField] float speedY = 13f;
    [SerializeField] float speedX = 10f;
    float movementSpeedX;

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
        if (Input.GetButtonDown("Fire1")) 
        { 
            animator.SetTrigger("CrouchTrigger"); 
        }

        bool crouch = Input.GetButton("Fire1");
        float movementSpeedY;
        //Check if player hit the ground
        Vector2 groundCheckPos = (Vector2)transform.position + new Vector2(0, gcPosY);
        Vector2 groundCheckScale = (Vector2)transform.localScale + new Vector2(gcScale, 0);
        bool ground = Physics2D.OverlapBox(groundCheckPos, groundCheckScale, 0, collisionLayer);
        //Tick down the timers
        groundTimer -= Time.deltaTime;
        jumpTriggerTimer -= Time.deltaTime;

        //jump timer and coyote time activated for smoother gameplay
        if (Input.GetButtonDown("Jump")) { jumpTriggerTimer = jumpTriggerTime; }
        if (ground) {groundTimer = groundTime; }

        //If jump was pressed, and player was touching the ground, jump!
        if ((jumpTriggerTimer > 0) && (groundTimer > 0))
        {
            groundTimer = 0;
            jumpTriggerTimer = 0;
            jumpTimer = jumpTime;
            booJump = true;
        }
        if (booJump && Input.GetButton("Jump")) 
        {
            if (jumpTimer > 0)
            {
                jumpTimer -= Time.deltaTime;
            }
            else 
            {
                booJump = false;
            }
        }
        if (Input.GetButtonUp("Jump")) 
        {
            booJump = false;
        }

            if (groundTimer > 0)
        {
            rigid.gravityScale = 0;
        }
        if (rigid.velocity.y > 0)
        {
            rigid.gravityScale = normalGravity;
        }
        else
        {
            rigid.gravityScale = fallGravity;
        }

        movementSpeedX = Input.GetAxisRaw("Horizontal") * speedX;
        if (Input.GetAxisRaw("Horizontal") > 0) 
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        movementSpeedY = rigid.velocity.y;
        ground = (groundTimer > 0);

        PlayAnimations(ground, crouch, movementSpeedX, movementSpeedY);
    }

    private void Move()
    {
        if (booJump)
        {
            //booJump = false;
            rigid.velocity = new Vector2(rigid.velocity.x, speedY);
        }
        rigid.velocity = new Vector2(movementSpeedX, rigid.velocity.y);
    }

    bool prevGround = false;
    private void PlayAnimations(bool ground, bool crouch,float MoveX, float MoveY) 
    {
        if (prevGround != ground)
        {
            animator.SetTrigger("GroundTrigger");
        }
        animator.SetBool("Ground", ground);
        animator.SetBool("Crouch", crouch);
        animator.SetFloat("MoveX", Mathf.Abs(MoveX));
        animator.SetFloat("MoveY", MoveY);

        prevGround = ground;
    }
}
