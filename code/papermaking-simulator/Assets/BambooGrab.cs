using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class BambooGrab : GrabAndThrow
{
    public GameObject newbamboo;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "ash")
        {
            Timer.Register(5f, () => OnChange());
        }
    }

    void OnChange()
    {
        Vector3 position = new Vector3((float)381.3, 6, (float)335.39);
        GameObject pooledBamboos = PhotonNetwork.Instantiate(newbamboo.name, position, gameObject.transform.rotation);
        gameObject.transform.localScale = new Vector3(0, 0, 0);
        pooledBamboos.GetComponent<Rigidbody>().isKinematic = false;
        PhotonNetwork.Destroy(gameObject.GetPhotonView());
    }
}
