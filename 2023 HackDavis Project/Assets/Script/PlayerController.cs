using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D body;
    private Vector2 moveDir;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        moveDir = new Vector2(moveX, moveY).normalized;

        if (moveDir.x > 0.0f)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (moveDir.x < 0.0f)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }

        body.velocity = new Vector2(moveDir.x * 5.0f, moveDir.y * 5.0f);    //Apply velocity
    }
}
