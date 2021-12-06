using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEvent : EventArgs
{
    public GameObject Object { get; private set; }
    public int Damage { get; private set; }

    public DamageEvent(GameObject obj, int damage)
    {
        Object = obj;
        Damage = damage;
    }
}
