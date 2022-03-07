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

    [SerializeField] private float attackDelay = 0.5f;
    [SerializeField] private GameObject attackFX;

    // The hitboxes of our 4 different directions
    [SerializeField] private GameObject hitBox_Top;
    [SerializeField] private GameObject hitBox_Bottom;
    [SerializeField] private GameObject hitBox_Left;
    [SerializeField] private GameObject hitBox_Right;


    [SerializeField] private bool canMove = true;

    public bool CanMove { get => canMove; set => canMove = value; }

    public void Awake()
    {
        boxcollider = GetComponent<BoxCollider2D>();//test
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public void Update()
    {
        HandleAttack();
        HandleMovement();
    }

    public void PlayAttackFX(Transform loc)
    {
        if(attackFX)
        {
            Instantiate(attackFX, loc);
            // sound fx
        }
    }

    public void PlayFX(GameObject fx)
    {
        if (fx)
        {
            Instantiate(fx, transform);
            // sound fx
        }
    }

    public void EnableAttackCollider()
    {
        if (animator.GetFloat("lastMoveX") == -1)
        {
            hitBox_Left.SetActive(true);
        }
        if (animator.GetFloat("lastMoveX") == 1)
        {
            hitBox_Right.SetActive(true);
        }
        if (animator.GetFloat("lastMoveY") == -1)
        {
            hitBox_Bottom.SetActive(true);
        }
        if (animator.GetFloat("lastMoveY") == 1)
        {
            hitBox_Top.SetActive(true);
        }
    }

    public void DisableAllAttackCollider()
    {
        hitBox_Left.SetActive(false);
        hitBox_Right.SetActive(false);
        hitBox_Bottom.SetActive(false);
        hitBox_Top.SetActive(false);
    }

    public void HandleAttack()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (CanMove)
            {
                StartCoroutine(Attack());
            }
        }
        else if (Input.GetKeyDown(KeyCode.G))
        {
            if (CanMove)
            {
                GetComponent<UltimateSkill>().Use();
            }
        }
    }

    IEnumerator Attack()
    {
        animator.SetTrigger("Attack");
        CanMove = false;

        // wait for 1 second after attack
        yield return new WaitForSeconds(attackDelay);
        CanMove = true;
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
        if (isIdle || !CanMove)
        {
            //Idle
            rigidbody2D.velocity = Vector2.zero;
            animator.SetBool("IsMoving", false);
        }
        else if (!isIdle && CanMove)
        {
            //Moving
            lastMoveDir = moveDir;
            rigidbody2D.velocity = moveDir * SPEED;
            animator.SetFloat("Horizontal", moveDir.x);
            animator.SetFloat("Vertical", moveDir.y);
            animator.SetBool("IsMoving", true);

            if (moveX == 1 || moveX == -1 || moveY == 1 || moveY == -1)
            {
                //if (canMove)
                {
                    animator.SetFloat("lastMoveX", moveX);
                    animator.SetFloat("lastMoveY", moveY);
                }
            }
        }

    }

}


