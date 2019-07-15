using System.Collections;

using System.Collections.Generic;

using UnityEngine;

public class Weapon : Item
{



    public int Damage { get; private set; }

    public Weapon(int id, string name, int num, int damage) :

        base(id, name, num)

    {

        this.Damage = damage;

    }

}