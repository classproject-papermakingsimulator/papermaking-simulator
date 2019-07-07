using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colliderController : MonoBehaviour
{

    Collider m_ObjectCollider;
    public Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        DisableRagdoll();
        //Fetch the GameObject's Collider (make sure they have a Collider component)
        m_ObjectCollider = GetComponent<Collider>();
        //Here the GameObject's Collider is not a trigger
        m_ObjectCollider.isTrigger = false;

    }

    // Let the rigidbody take control and detect collisions.
    void EnableRagdoll()
    {
        rb.isKinematic = false;
        rb.detectCollisions = true;
    }

    // Let animation control the rigidbody and ignore collisions.
    void DisableRagdoll()
    {
        rb.isKinematic = true;
        rb.detectCollisions = false;
    }
}
