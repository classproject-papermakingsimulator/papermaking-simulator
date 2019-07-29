using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drypaper : GrabAndThrow
{
    public GameObject writepaper;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "desk")
        {
            GameObject.Find("drypaper").GetComponent<drypaper>().writepaper.SetActive(true);
            gameObject.transform.localScale = new Vector3(0, 0, 0);
            Destroy(gameObject, 2f);
        }
    }
}
