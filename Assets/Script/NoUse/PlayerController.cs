using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float SPEED = 0.5f;

    public new Rigidbody2D rigidbody2D;
    public Vector2 moveDir;
    public Vector2 lastMoveDir;
    public Animator animator;
    public BoxCollider2D boxcollider;//test

    public void Awake()
    {
        boxcollider = GetComponent<BoxCollider2D>();//test
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public void Update()
    {
        HandleMovement();
    }

    public void HandleMovement()
    {
        float moveX = 0f;
        float moveY = 0f;

        if (Input.GetKey(KeyCode.W))
        {
            moveY = +1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveY = -1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveX = -1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveX = +1f;
        }

        Vector2 moveDir = new Vector2(moveX, moveY).normalized;

        bool isIdle = moveX == 0 && moveY == 0;
        //animator.SetBool("IsMoving", !isIdle);
        if (isIdle)
        {
            //Idle
            rigidbody2D.velocity = Vector2.zero;
            animator.SetBool("IsMoving", false);
        }
        else
        {
            //Moving
            lastMoveDir = moveDir;
            rigidbody2D.velocity = moveDir * SPEED;
            animator.SetFloat("Horizontal", moveDir.x);
            animator.SetFloat("Vertical", moveDir.y);
            animator.SetBool("IsMoving", true);
        }

    }

}


