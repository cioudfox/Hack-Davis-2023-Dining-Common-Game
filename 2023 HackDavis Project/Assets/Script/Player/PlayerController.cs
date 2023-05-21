using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D body;
    private Vector2 moveDir;
    public Animator animator;
    private float lastHorizontalVector;
    private float lastVerticalVector;
    private Vector2 lastMovedVector;
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        lastMovedVector = new Vector2(1, 0f); //If we don't do this and game starts up and don't move, the projectile weapon will have no momentum
    }

    // Update is called once per frame
    void Update()
    {
        InputManager();
        Move();
        // itemCooldownTimer -= Time.deltaTime;
    }

    void InputManager()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        moveDir = new Vector2(moveX, moveY).normalized; //Use normalize as moving in diagonal generates a value > 1 so cap it to 1

        if(moveDir.x != 0)
        {
            lastHorizontalVector = moveDir.x;
            lastMovedVector = new Vector2(lastHorizontalVector, 0f);    //Last moved X
        }

        if(moveDir.y != 0)
        {
            lastVerticalVector = moveDir.y;
            lastMovedVector = new Vector2(0f, lastVerticalVector);      //Last moved Y
        }

        if(moveDir.x != 0 && moveDir.y != 0)
        {
            lastMovedVector = new Vector2(lastHorizontalVector, lastVerticalVector);    //While moving
        }

        // if (GameObject.FindGameObjectWithTag("Inventory") == null)
        // {
        //     if (Input.GetMouseButtonDown(0))
        //     {
        //         // Left mouse button was clicked
        //         this.leftMouse.Execute(this.gameObject);
        //     }
        //     if (Input.GetMouseButtonDown(1))
        //     {
        //         // Right mouse button was clicked
        //         this.rightMouse.Execute(this.gameObject);
        //     }
        // }

        // if (Input.GetKeyDown(KeyCode.I))
        // {
        //     // The "I" key was pressed to toggle inventory
        //     this.Ibutton.Execute(this.gameObject);
        // }
    }
    void Move()
    {
        if (moveDir.x != 0 || moveDir.y != 0)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        if (moveDir.x > 0.0f)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (moveDir.x < 0.0f)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }

        body.velocity = new Vector2(moveDir.x * 5.0f, moveDir.y * 5.0f);    //Apply velocity
    }
}
