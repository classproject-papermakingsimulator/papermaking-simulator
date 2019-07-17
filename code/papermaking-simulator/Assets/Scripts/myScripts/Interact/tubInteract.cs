using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tubInteract : MonoBehaviour
{
    public InventoryAdd inventory;
    private int count;

    public void filter()
    {
        inventory.minus();
        inventory.add();
    }

}
