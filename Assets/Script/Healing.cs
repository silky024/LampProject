using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healing : Collectable
{
    HealthPoint health;

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<HealthPoint>().Heal(20);
        }
    }
    protected override void Oncollect()
    {
        Debug.Log("Healing");
        gameObject.SetActive(false);
    }

}
