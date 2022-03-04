using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    #region Public Variables 

    public bool canMove = true;
    public float enemyKnockBackThrust = 15f;

    #endregion

    #region Private Variables

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed;
    [SerializeField] private SpriteRenderer spriteRenderer;
    // Used in PlayerHealth collision with Enemies
    [SerializeField] public int damageDoneToHero;
    [SerializeField] private float attackSpeed = 1f;
    private int xDir;
    private int yDir;
    private float randomNum;
    private Vector2 movement;
    private float canAttack;
    private Transform target;
    #endregion
    public float speed = 3f;



    private void Start()
    {
        StartCoroutine(ChangeDirectionRoutine());
    }

    private void Update()
    {
        if (target != null)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, target.position, step);
        }
        Move();
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (attackSpeed <= canAttack)
            {
                other.gameObject.GetComponent<HealthPoint>().DamagePlayer(30);
                canAttack = 0f;
            }
            else
            {
                canAttack += Time.deltaTime;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            target = other.transform;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            target = null;
        }
    }

    private void Move()
    {
        if (!canMove) { return; }

        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        // can flip sprite to reduce amount of sprites & animations
        if (movement.x == 1)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }

    // random movement around map
    private IEnumerator ChangeDirectionRoutine()
    {
        while (true)
        {
            randomNum = Random.Range(-5, 5);
            movement.x = Random.Range(-1, 2);
            movement.y = Random.Range(-1, 2);
            yield return new WaitForSeconds(randomNum);
        }
    }
}
