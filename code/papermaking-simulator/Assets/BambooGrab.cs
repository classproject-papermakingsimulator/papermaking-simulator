using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class BambooGrab : VRTK_InteractableObject
{
    public GameObject projectile;
    public Camera body;
    public InventoryAdd inventoryAdd;
    public float projectileSpeed = 1000f;

    protected virtual void InteractableObjectUsed(object sender, InteractableObjectEventArgs e)
    {
        inventoryAdd = GameObject.Find("GridImage").GetComponent<InventoryAdd>();
        if(inventoryAdd.minus())
        {
            if (projectile != null)
            {
                GameObject projectileClone = Instantiate(projectile, body.transform.position, body.transform.rotation) as GameObject;
                Rigidbody projectileRigidbody = projectile.GetComponent<Rigidbody>();
                if (projectileRigidbody != null)
                {
                    projectileRigidbody.AddForce(projectile.transform.forward * projectileSpeed);
                }
            }
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
