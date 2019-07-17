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

    public void cutdown()
    {
        count++;
        print(count);
        isDown();
    }

    private void isDown()
    {
        if(count == 10)
        {
            count = 0;
            //inventory.??
            gameObject.SetActive(false);
            inventory.add();
        }
    }
}
