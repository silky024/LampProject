using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimateSkill : MonoBehaviour
{
    [SerializeField] GameObject damageCollider;
    [SerializeField] float chargeTime = 2.0f;
    [SerializeField] float activeTime = 1.0f;

    [SerializeField] GameObject lightFX;
    [SerializeField] GameObject chargeFX;
    [SerializeField] GameObject activeFX;
    [SerializeField] GameObject finishFX;

    private PlayerController player;

    private void Awake()
    {
        player = GetComponent<PlayerController>();
    }
   

    public void Use()
    {
        if (player.CanMove)
        {
            StartCoroutine(ChargeState());
        }       
    }

    IEnumerator ChargeState()
    {
        
        player.CanMove = false; // prevent from moving

        //--- Charging...
        GetComponent<Animator>().SetTrigger("ulti_charge");
        GameObject p1 = Instantiate(chargeFX, transform);

        yield return new WaitForSeconds(0.2f);
        lightFX.SetActive(true);
        yield return new WaitForSeconds( chargeTime );
        Destroy(p1);


        //--- Active!...
        GetComponent<Animator>().SetTrigger("ulti_active");
        GameObject p2 = Instantiate(activeFX, transform);
        damageCollider.SetActive(true);
        yield return new WaitForSeconds(activeTime);


        //--- Finially...
        lightFX.SetActive(false);
        player.CanMove = true;
        damageCollider.SetActive(false);
        Instantiate(finishFX, transform);
        Destroy(p2);

        yield return null;
    }

}
