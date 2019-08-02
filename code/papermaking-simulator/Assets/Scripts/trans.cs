using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class trans : GrabAndThrow
{
    public GameObject second;
    public GameObject third;
    public GameObject thisOne;
    public GameObject counter;
    private int count;
    private bool hammerable;

    protected override void OnEnable()
    {
        linkedObject = (linkedObject == null ? GetComponent<VRTK_InteractableObject>() : linkedObject);

        if (linkedObject != null)
        {
            linkedObject.InteractableObjectUsed += InteractableObjectUsed;
        }
        count = 0;
        hammerable = false;
        counter = GameObject.Find("Environment/Pail/pailcounter");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (counter == null)
            counter = GameObject.Find("Environment/Pail/pailcounter");
        if (collision.collider.name == "Pail" && !hammerable)
        {
            Vector3 tmp = new Vector3((float)268.6018, (float)0.3, (float)325.1262);
            gameObject.transform.position = tmp;
            gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
            hammerable = true;
        }
        if (collision.collider.name == "sledgeHammer" && hammerable)
        {
            float newx = counter.transform.localPosition.x + 1;
            counter.transform.localPosition = new Vector3(newx, counter.transform.localPosition.y, 0);
            changeObject();
            Vector3 temp = new Vector3(gameObject.GetComponent<Transform>().position.x, gameObject.GetComponent<Transform>().position.y, gameObject.GetComponent<Transform>().position.z);
            Vector3 temp2 = new Vector3(0, 0, 0);
            gameObject.GetComponentInParent<destroyMash>().changeTrans(temp);
            gameObject.transform.localPosition = temp2;

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "tub" && thisOne.name == "Capsule")
        {
            gameObject.GetComponentInParent<destroyMash>().DestroyIt();
        }
    }

    private void changeObject()
    {
        count = (int)counter.transform.localPosition.x;
        if (count >= 10)
        {
            if (thisOne.name == "Cube")
            {
                Vector3 tmp = new Vector3((float)268.6018, (float)0.3, (float)325.1262);
                second.transform.position = tmp;
                second.transform.rotation = new Quaternion(0, 0, 0, 0);
                second.SetActive(true);
                thisOne.SetActive(false);
                counter.transform.localPosition = new Vector3(0, 1, 0);
            }
            if (thisOne.name == "Sphere")
            {
                Vector3 tmp = new Vector3((float)268.6018, (float)0.3, (float)325.1262);
                third.transform.position = tmp;
                third.transform.rotation = new Quaternion(0, 0, 0, 0);
                third.SetActive(true);
                thisOne.SetActive(false);
                counter.transform.localPosition = new Vector3(0, 2, 0);
            }
        }
        switch ((int)counter.transform.localPosition.y)
        {
            case 1:
                {
                    if (thisOne.name == "Cube")
                    {
                        Vector3 tmp = new Vector3((float)268.6018, (float)0.3, (float)325.1262);
                        second.transform.position = tmp;
                        second.transform.rotation = new Quaternion(0, 0, 0, 0);
                        second.SetActive(true);
                        thisOne.SetActive(false);
                    }
                    break;
                }

            case 2:
                {
                    if (thisOne.name == "Sphere")
                    {
                        Vector3 tmp = new Vector3((float)268.6018, (float)0.3, (float)325.1262);
                        third.transform.position = tmp;
                        third.transform.rotation = new Quaternion(0, 0, 0, 0);
                        third.SetActive(true);
                        thisOne.SetActive(false);
                    }
                    break;
                }
            default:
                break;
        }
    }
}
