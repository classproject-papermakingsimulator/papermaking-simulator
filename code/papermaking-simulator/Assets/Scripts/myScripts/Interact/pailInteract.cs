using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pailInteract : MonoBehaviour
{
    public InventoryAdd inventory;
    private int count;

    public void pail()
    {
        inventory.minus();
        inventory.add();
    }
}
