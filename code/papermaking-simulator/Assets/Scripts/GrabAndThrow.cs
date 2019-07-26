using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class GrabAndThrow : VRTK_InteractableObject
{
    public VRTK_InteractableObject linkedObject;
    public GameObject projectile;
    private UImanager canva;
    GameObject temp;
    public Transform body;
    public InventoryAdd inventoryAdd;
    public float projectileSpeed = 1000f;
    public bool pickable = true;

    protected virtual void InteractableObjectUsed(object sender, InteractableObjectEventArgs e)
    {
        pickable = false;
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
        Timer.Register(2f, () => { pickable = true; });

    }

    public void pick(String name)
    {
        if (pickable)
        {
            if (name.Equals("Bamboo/staticBamboo"))
            {
                inventoryAdd = GameObject.Find(name).GetComponent<BambooGrab>().inventoryAdd;
                inventoryAdd.add();
                Destroy(gameObject);
            }
            if (name.Equals("Bamboo/staticBamboo2"))
            {
                inventoryAdd = GameObject.Find(name).GetComponent<pooledBamboo>().inventoryAdd;
                inventoryAdd.add();
                Destroy(gameObject);
            }
            if (name.Equals("mash/Cube"))
            {
                inventoryAdd = GameObject.Find(name).GetComponent<trans>().inventoryAdd;
                inventoryAdd.add();
                gameObject.GetComponentInParent<destroyMash>().DestroyIt();
            }
            if (name.Equals("mash/Capsule"))
            {
                inventoryAdd = GameObject.Find(name).GetComponent<trans>().inventoryAdd;
                inventoryAdd.add();
                gameObject.GetComponentInParent<destroyMash>().DestroyIt();
            }
            if (name.Equals("wetpaper"))
            {
                inventoryAdd = GameObject.Find(name).GetComponent<wetpaperinteract>().inventoryAdd;
                inventoryAdd.add();
                Destroy(gameObject);
            }


        }
       
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

    public void getOne()
    {
        if (inventoryAdd.selfMinus())
        {
            if (projectile != null)
            {       
                canva = GameObject.Find("Canvas").GetComponent<UImanager>();
                GameObject projectileClone = Instantiate(projectile, body.transform.position, body.transform.rotation) as GameObject;
                projectileClone.SetActive(true);
                canva.InventoryButton();
                Rigidbody projectileRigidbody = projectile.GetComponent<Rigidbody>();
                if (projectileRigidbody != null)
                {
                    projectileRigidbody.isKinematic = true;
                    
                }
            }
        }
    }


}
