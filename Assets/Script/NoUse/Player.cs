using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rb;
    public BoxCollider2D boxcollider;
    public Vector3 moveDelta;
    public RaycastHit2D hit;
    Vector2 movement;

    public void Start()
    {
        boxcollider = GetComponent<BoxCollider2D>();
    }

    public void FixedUpdate()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        //Reset moveDelta
        moveDelta = new Vector3(movement.x, movement.y, 0);

        //Swap player direction
        if (moveDelta.x > 0)
            transform.localScale = Vector3.one;
        else if (moveDelta.x < 0)
            transform.localScale = new Vector3(-1, 1, 15);

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
        rb.MovePosition(rb.position + movement * moveDelta * Time.fixedDeltaTime);

    }
}
