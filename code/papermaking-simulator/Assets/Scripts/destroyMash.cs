using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class destroyMash : MonoBehaviour
{
    public void DestroyIt()
    {
        PhotonNetwork.Destroy(gameObject);
    }

    public void changeTrans(Vector3 temp)
    {
        gameObject.transform.position = temp;
    }
}
