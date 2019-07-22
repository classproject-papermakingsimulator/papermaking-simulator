using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class transform : MonoBehaviour
{
    public GameObject second;
    public GameObject third;
    public GameObject thisOne;
    public InventoryAdd inventory;
    private int count;
    // Start is called before the first frame update
    void onEnable()
    {
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public void pick()
    {
        inventory.add();
        Destroy(thisOne);
    }
}
