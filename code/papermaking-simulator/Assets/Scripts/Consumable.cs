using System.Collections;

using System.Collections.Generic;

using UnityEngine;

public class Consumable : Item

{

    public int BackHp { get; private set; }

    public int BackMp { get; private set; }

    public Consumable(int id, string name, int num, int backHp, int backMp) :

        base(id, name, num)

    {

        this.BackHp = backHp;

        this.BackMp = backMp;

        

    }

}