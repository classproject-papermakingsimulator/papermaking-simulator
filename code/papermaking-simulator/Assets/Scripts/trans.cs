using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class trans : GrabAndThrow
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
        if(collision.collider.name == "sledgeHammer")
        {
            count++;
            changeObject();
            Vector3 temp = new Vector3(gameObject.GetComponent<Transform>().position.x, gameObject.GetComponent<Transform>().position.y, gameObject.GetComponent<Transform>().position.z);
            Vector3 temp2 = new Vector3(0, 0, 0);
            gameObject.GetComponentInParent<destroyMash>().changeTrans(temp);
            gameObject.transform.localPosition = temp2;

        }
    }

    private void changeObject()
    {
        if(count >= 10)
        {
            
            if (thisOne.name == "Cube")
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
