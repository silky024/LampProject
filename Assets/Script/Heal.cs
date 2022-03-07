using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    HealthPoint health;
     
    [SerializeField] private GameObject healFX;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<HealthPoint>().Heal(10);
            other.gameObject.GetComponent<PlayerController>().PlayFX(healFX);
        }
        gameObject.SetActive(false);
    }

}
