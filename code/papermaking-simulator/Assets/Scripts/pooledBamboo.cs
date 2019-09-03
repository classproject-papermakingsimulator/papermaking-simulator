using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pooledBamboo : GrabAndThrow
{
    
    public boilInteract boil;

    private void OnCollisionEnter(Collision collision)
    {
        boil = GameObject.Find("furnace/Barrel").GetComponent<boilInteract>();
        if(collision.collider.tag == "boil")
        {
            if(boil.boil())
            {
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }

}
