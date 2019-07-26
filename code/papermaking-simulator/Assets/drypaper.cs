using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drypaper : GrabAndThrow
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "desk")
        {
            Vector3 pos = new Vector3((float)273.422, (float)1.1562, (float)303.4507);
            Quaternion rot = new Quaternion((float)90.00001, 0, (float)31.797, 0);
            Vector3 sca = new Vector3((float)1.241851, (float)1.46287, (float)0.01000002);
            gameObject.transform.position = pos;
            gameObject.transform.rotation = rot;
            gameObject.transform.localScale = sca;
            Rigidbody tmp = gameObject.GetComponent<Rigidbody>();
            tmp.constraints = RigidbodyConstraints.FreezeAll;
        }
    }
}
