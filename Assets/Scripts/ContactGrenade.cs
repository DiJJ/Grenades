using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactGrenade : Grenade
{
    private void OnCollisionEnter(Collision collision)
    {
        Explode();
    }
}
