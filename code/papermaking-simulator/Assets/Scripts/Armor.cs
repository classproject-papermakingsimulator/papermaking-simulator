using System.Collections;

using System.Collections.Generic;

using UnityEngine;

public class Armor : Item

{

    public int Power { get; private set; }

    public int Defend { get; private set; }

    public int Agility { get; private set; }

    public Armor(int id, string name, int num, int power, int defend, int agility) :

        base(id, name, num)

    {

        this.Power = power;

        this.Defend = defend;

        this.Agility = agility;

        

    }

}