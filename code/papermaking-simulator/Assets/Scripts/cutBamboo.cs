using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cutBamboo : MonoBehaviour
{

    bambooInteract bam = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.tag == "bamboo")
        {
            GameObject.Find(collider.name).GetComponent<bambooInteract>().cutdown();
        }
    }

}
