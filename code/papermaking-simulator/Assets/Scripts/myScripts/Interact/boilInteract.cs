using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class boilInteract : MonoBehaviour
{
    public GameObject mash;
    private bool isboil = false;

    public bool boil()
    {
        if(isboil)
        {
            return false;
        }
        else
        {
            Timer.Register(5f, complete);
            return true;
        }
    }

    private void complete()
    {
        Vector3 position = new Vector3((float)268.6058, 2, (float)325.2);
        GameObject pooledBamboos = PhotonNetwork.Instantiate(mash.name, position, gameObject.transform.rotation);
        pooledBamboos.GetComponentInChildren<Rigidbody>().isKinematic = false;
        //intoBoil = GameObject.Find("furnace/Barrel").GetComponent<boilInteract>();
        //intoBoil.boil();
    }
}
