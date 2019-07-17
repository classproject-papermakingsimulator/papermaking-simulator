using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BambooLock : MonoBehaviour
{
    private bool into = false;
       
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        into = true;
    }

    private void OnTriggerExit(Collider other)
    {
        into = false;
    }

    public bool getInto()
    {
        return into;
    }


}
