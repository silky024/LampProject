using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPoint : MonoBehaviour
{
    
    public int maxHealth = 100;
    public int curHealth = 100;

    public HealthBar healthBar;

    [SerializeField] private GameObject dieFX;

    void Start()
    {
        curHealth = maxHealth;
    }

    public void UpdateHealth()
    {

        if (curHealth > maxHealth)
        {
            curHealth = maxHealth;
        }
        else if (curHealth <= 0)
        {
            curHealth = 0;
            Die();
        }
    }
    public void TakeDamage(int damage)
    {
        curHealth -= damage;
        UpdateHealth();
    }

    public void Heal(int heal)
    {
        curHealth += heal;
        UpdateHealth();
    }

    void UpdateHealthBar()
    {

        if (healthBar)
            healthBar.SetHealth(curHealth);
    }

    public void Die()
    {

        if(dieFX)
        {
            Instantiate(dieFX, transform);

            //disable rendering component
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<CircleCollider2D>().enabled = false;

            // then destroy the object after 1 sec
            Destroy(gameObject, 1);
        }
        else
        {
            Destroy(gameObject);
        }

            
    }

}
