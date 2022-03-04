using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public const float SPEED = 1.5f;
    BoxCollider2D boxcollider;
    public new Rigidbody2D rigidbody2D;
    public Vector2 moveDir;
    public Vector2 lastMoveDir;
    public Animator animator;
    Vector2 movement;
    Vector3 moveDelta;
    RaycastHit2D hit;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Awake()
    {
        boxcollider = GetComponent<BoxCollider2D>();

        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public void Update()
    {
        HandleMovement();
    }

    private void FixedUpdate()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        //Reset moveDelta
        moveDelta = new Vector3(movement.x, movement.y, 0);

       /* //Swap player direction
        if (moveDelta.x > 0)
            transform.localScale = Vector3.one;
        else if (moveDelta.x < 0)
            transform.localScale = new Vector3(-1, 1, 15);*/

        hit = Physics2D.BoxCast(transform.position, boxcollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Action", "Blocking"));
        if (hit.collider == null)
        {
            //Make this thing move
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
        }
        hit = Physics2D.BoxCast(transform.position, boxcollider.size, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Action", "Blocking"));
        if (hit.collider == null)
        {
            //Make this thing move
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
        }
    }


    public void HandleMovement()
    {
        float moveX = 0f;
        float moveY = 0f;


        Vector2 moveDir = new Vector2(moveX, moveY).normalized;

        bool isIdle = moveX == 0 && moveY == 0;
        //animator.SetBool("isMoving", !isIdle);
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
