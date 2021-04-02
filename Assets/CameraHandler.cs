using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    Rigidbody2D rigid;
    [SerializeField] Vector2 speed;
    Vector2 movement;

    //Movement Limits
    bool moveLeft = true;
    bool moveRight = true;
    bool moveUp = true;
    bool moveDown = true;

    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rigid.velocity = movement;
    }

    void Update()
    {

    }

    public void OnChildTriggerEnter(string child, Collider2D collision) 
    {
        if (collision.gameObject.CompareTag("Border"))
        {
            switch (child)
            {
                case "TriggerLeft":
                    if (movement.x < 0) movement.x = 0;
                    moveLeft = false;
                    break;
                case "TriggerRight":
                    if (movement.x > 0) movement.x = 0;
                    moveRight = false;
                    break;
                case "TriggerUp":
                    if (movement.y < 0) movement.y = 0;
                    Debug.Log("Upper limit triggered");
                    moveUp = false;
                    break;
                case "TriggerDown":
                    if (movement.y > 0) movement.y = 0;
                    Debug.Log("Lower limit triggered");
                    moveDown = false;
                    break;
            }
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            switch (child)
            {
                case "TriggerLeft":
                    if (moveLeft) movement.x = -speed.x;
                    break;
                case "TriggerRight":
                    if (moveRight) movement.x = speed.x;
                    break;
                case "TriggerUp":
                    if (moveUp) movement.y = speed.y;
                    break;
                case "TriggerDown":
                    if (moveDown) movement.y = -speed.y;
                    break;
            }
        }
    }

    public void OnChildTriggerStay(string child, Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Border"))
        {
            switch (child)
            {
                case "TriggerLeft":
                    if (movement.x < 0) movement.x = 0;
                    moveLeft = false;
                    break;
                case "TriggerRight":
                    if (movement.x > 0) movement.x = 0;
                    moveRight = false;
                    break;
                case "TriggerUp":
                    if (movement.y < 0) movement.y = 0;
                    moveUp = false;
                    break;
                case "TriggerDown":
                    if (movement.y > 0) movement.y = 0;
                    moveDown = false;
                    break;
            }
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            switch (child)
            {
                case "TriggerLeft":
                    if(moveLeft) movement.x = -speed.x;
                    break;
                case "TriggerRight":
                    if (moveRight) movement.x = speed.x;
                    break;
                case "TriggerUp":
                    if (moveUp) movement.y = speed.y;
                    break;
                case "TriggerDown":
                    if (moveDown) movement.y = -speed.y;
                    break;
            }
        }

    }
    public void OnChildTriggerExit(string child, Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Border"))
        {
            switch (child)
            {
                case "TriggerLeft":
                    moveLeft = true;
                    break;
                case "TriggerRight":
                    moveRight = true;
                    break;
                case "TriggerUp":
                    moveUp = true;
                    break;
                case "TriggerDown":
                    moveDown = true;
                    break;
            }
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            switch (child)
            {
                case "TriggerLeft":
                    movement.x = 0;
                    break;
                case "TriggerRight":
                    movement.x = 0;
                    break;
                case "TriggerUp":
                    movement.y = 0;
                    break;
                case "TriggerDown":
                    movement.y = 0;
                    break;
            }
        }
    }
}
