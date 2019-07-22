using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class BambooGrab : VRTK_InteractableObject
{
    public Transform body;
    public InventoryAdd inventoryAdd;
    public float projectileSpeed = 1000f;

    protected virtual void InteractableObjectUsed(object sender, InteractableObjectEventArgs e)
    {
        inventoryAdd = GameObject.Find("GridImage").GetComponent<InventoryAdd>();
        body = GameObject.Find("Controller(right)").GetComponent<Transform>();
        if(inventoryAdd.minus())
        {
            isGrabbable = false;
            Rigidbody projectileRigidbody = gameObject.GetComponent<Rigidbody>();
            if (projectileRigidbody != null)
            {
                projectileRigidbody.AddForce(body.transform.forward * projectileSpeed);
            }
            isGrabbable = true;
        }
        else
        {
            inventoryAdd.add();
        }
    }

    public void pick()
    {
        inventoryAdd = GameObject.Find("GridImage").GetComponent<InventoryAdd>();
        inventoryAdd.add();
        Destroy(gameObject);
    }

}
