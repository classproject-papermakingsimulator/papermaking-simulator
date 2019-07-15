using System.Collections;

using System.Collections.Generic;

using UnityEngine;

public class Item
{

    public int Id { get; private set; }

    public string Name { get; private set; }


    public int Num { get; private set; }


    public Item(int id, string name,int num)
    {

        this.Id = id;

        this.Name = name;

        this.Num = num;

    }
    public void SetNum(int num)
    {
        this.Num = num;
    }

    public void AddNum()
    {
        this.Num++;
    }

    public bool DeleteNum()
    {
        if (this.Num <= 0)
            return false;
        
        this.Num--;
        return true;
    }
}