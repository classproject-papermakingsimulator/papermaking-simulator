using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class transform : GrabAndThrow
{
    public GameObject second;
    public GameObject third;
    public GameObject thisOne;
    private int count;
    protected override void OnEnable()
    {
        linkedObject = (linkedObject == null ? GetComponent<VRTK_InteractableObject>() : linkedObject);

        if (linkedObject != null)
        {
            linkedObject.InteractableObjectUsed += InteractableObjectUsed;
        }
        count = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        count++;
        changeObject();
    }

    private void changeObject()
    {
        if(count >= 10)
        {
            if(thisOne.name == "Cube")
            {
                second.SetActive(true);
                thisOne.SetActive(false);
            }
            if (thisOne.name == "Sphere")
            {
                third.SetActive(true);
                thisOne.SetActive(false);
            }
            //if (thisOne.name == "Capsule")
            //{
            //    inventory.minus();
            //    inventory.add();
            //}
        }
    }
}
