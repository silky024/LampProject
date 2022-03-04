using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : Colliable
{
    protected bool colloected;

    protected override void Oncollide(Collider2D coll)
    {
        if (coll.name == "Player")
        {
            Oncollect();
        }
    }

    protected virtual void Oncollect()
    {
        colloected = true;
    }
}
