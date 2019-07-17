using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pressInteract : MonoBehaviour
{
    public InventoryAdd inventory;
    private int count;

    public void press()
    {
        inventory.minus();
        inventory.add();
    }
}
