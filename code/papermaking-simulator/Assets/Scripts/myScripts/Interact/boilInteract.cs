using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boilInteract : MonoBehaviour
{
    public InventoryAdd inventory;
    private int count;

    public void boil()
    {
        inventory.minus();
        inventory.add();
    }
}
