﻿using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pooledBamboo : GrabAndThrow
{
    private boilInteract intoBoil;
    public GameObject mash;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "boil")
        {
            gameObject.transform.localScale = new Vector3(0, 0, 0);
            Timer.Register(5f, () => toBoil());
        }
    }

    private void toBoil()
    {
        Vector3 position = new Vector3((float)268.6058, 2, (float)325.2);
        GameObject pooledBamboos = PhotonNetwork.Instantiate(mash.name, position, gameObject.transform.rotation);
        pooledBamboos.GetComponentInChildren<Rigidbody>().isKinematic = false;
        //intoBoil = GameObject.Find("furnace/Barrel").GetComponent<boilInteract>();
        //intoBoil.boil();
        PhotonNetwork.Destroy(gameObject);
    }
}
