using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cutInteract : MonoBehaviour
{
    public InventoryAdd inventory;
    private int count;

    public void cut()
    {
        inventory.minus();
        inventory.add();
    }
}
