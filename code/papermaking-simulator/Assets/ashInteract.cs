using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ashInteract : MonoBehaviour
{
    public InventoryAdd inventory;
    private int count;

    public void dry()
    {
        inventory.minus();
        inventory.add();
    }
}
