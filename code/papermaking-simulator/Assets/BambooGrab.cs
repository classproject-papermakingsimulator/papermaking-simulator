using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class BambooGrab : VRTK_InteractableObject
{
    public VRTK_InteractableObject linkedObject;
    GameObject temp;
    public Transform body;
    public InventoryAdd inventoryAdd;
    public float projectileSpeed = 1000f;

    protected virtual void InteractableObjectUsed(object sender, InteractableObjectEventArgs e)
    {
  
        temp = GameObject.Find("VR/[VRTK_SDKManager]/[VRTK_SDKSetups]/SteamVR/[CameraRig]/Controller (right)");
        body = temp.GetComponent<Transform>();
        StopGrabbingInteractions();
        isGrabbable = false;
        Rigidbody projectileRigidbody = gameObject.GetComponent<Rigidbody>();
        if (projectileRigidbody != null)
        {
            projectileRigidbody.isKinematic = false;
            projectileRigidbody.AddForce(body.transform.forward * projectileSpeed);
        }
        isGrabbable = true;
       
    }

    public void pick()
    {
        inventoryAdd = GameObject.Find("Bamboo/b1").GetComponent<bambooInteract>().inventory;
        inventoryAdd.add();
        Destroy(gameObject);
    }

    protected virtual void OnEnable()
    {
        linkedObject = (linkedObject == null ? GetComponent<VRTK_InteractableObject>() : linkedObject);

        if (linkedObject != null)
        {
            linkedObject.InteractableObjectUsed += InteractableObjectUsed;
        }
    }

    protected virtual void OnDisable()
    {
        if (linkedObject != null)
        {
            linkedObject.InteractableObjectUsed -= InteractableObjectUsed;
        }
    }

}
