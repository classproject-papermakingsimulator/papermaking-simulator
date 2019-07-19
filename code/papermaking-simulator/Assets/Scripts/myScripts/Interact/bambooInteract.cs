using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bambooInteract : MonoBehaviour
{
    public InventoryAdd inventory;
    private int count;

    private void Awake()
    {
        count = 0;
    }

    public void cutdown(double v)
    {
        if (v < 10)
            print("low");
        else if (v > 20)
            print("large");
        else if (v > 16)
        {
            count += 2;
            print(count);
            isDown();
        }
        else
        {
            count ++;
            print(count);
            isDown();
        }

    }

    private void isDown()
    {
        if(count == 10 || count == 11)
        {
            count = 0;
            //inventory.??
            gameObject.SetActive(false);
            inventory.add();
        }
    }
}
