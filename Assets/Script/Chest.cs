using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectable
{
    public Sprite emptyChest;
    public int goldAmount = 10;

    protected override void Oncollect()
    {
        if (!colloected)
        {
            colloected = true;
            GetComponent<SpriteRenderer>().sprite = emptyChest;
            Debug.Log("Get Gold " + goldAmount + " Gold!!");
        }
    }
}
