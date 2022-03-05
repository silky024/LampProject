using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDamage : MonoBehaviour
{

    [SerializeField] private PlayerController player;
    [SerializeField] private int damageAmount = 1;
    

    private const string enemyString = "Enemy";
    private const string playerString = "Player";
  

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyAttack(other);
    }

    private void EnemyAttack(Collider2D other)
    {
        if (other.gameObject.CompareTag(enemyString))
        {
            other.GetComponent<HealthPoint>().TakeDamage(damageAmount);
            player.PlayAttackFX(other.gameObject.transform);

        }
    }
}
