using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPoint : MonoBehaviour
{
    public int curHealth = 0;
    public int maxHealth = 100;

    public HealthBar healthBar;

    void Start()
    {
        curHealth = maxHealth;
    }

    public void UpdateHealth(int mod)
    {
        curHealth += mod;

        if (curHealth > maxHealth)
        {
            curHealth = maxHealth;
        }
        else if (curHealth <= 0)
        {
            curHealth = 0;
            Debug.Log("Player Respawn");
        }
    }
    public void DamagePlayer(int damage)
    {
        curHealth -= damage;

        healthBar.SetHealth(curHealth);
    }

    public void HealingPlayer(int heal)
    {
        curHealth += heal;
        healthBar.SetHealth(curHealth);
    }

}
