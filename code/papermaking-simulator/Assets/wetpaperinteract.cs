using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wetpaperinteract : GrabAndThrow
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "dry")
        {
            Destroy(gameObject);
        }
    }
}
