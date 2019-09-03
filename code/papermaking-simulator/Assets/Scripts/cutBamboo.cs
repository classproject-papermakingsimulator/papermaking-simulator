using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class cutBamboo : VRTK_InteractableObject
{
    bambooInteract bam = null;
    private float impactMagnifier = 120f;
    private float collisionForce = 0f;
    private float maxCollisionForce = 1000f;
    private VRTK_ControllerReference controllerReference;
    private bool hasCollided = false;

    public float CollisionForce()
    {
        return collisionForce;
    }

    public override void Grabbed(VRTK_InteractGrab grabbingObject)
    {
        base.Grabbed(grabbingObject);
        controllerReference = VRTK_ControllerReference.GetControllerReference(grabbingObject.controllerEvents.gameObject);
    }

    public override void Ungrabbed(VRTK_InteractGrab previousGrabbingObject)
    {
        base.Ungrabbed(previousGrabbingObject);
        controllerReference = null;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        controllerReference = null;
        interactableRigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (this.hasCollided == true) { return; }
        this.hasCollided = true;
        Vector3 v = gameObject.GetComponent<Rigidbody>().velocity;
        double vm = Mathf.Sqrt(v.x * v.x + v.y * v.y + v.z * v.z);
        if (VRTK_ControllerReference.IsValid(controllerReference) && IsGrabbed())
        {
            if (collision.collider.tag == "bamboo")
            {
                print(collision.collider.name);
                GameObject.Find(collision.collider.name).GetComponent<bambooInteract>().cutdown(vm);
                ContactPoint contactPoint = collision.contacts[0];
                Vector3 newDir = Vector3.zero;
                Vector3 curDir = transform.TransformDirection(Vector3.forward);
                newDir = Vector3.Reflect(curDir, contactPoint.normal);
                Quaternion rotation = Quaternion.FromToRotation(Vector3.forward, newDir);
            }
            collisionForce = VRTK_DeviceFinder.GetControllerVelocity(controllerReference).magnitude * impactMagnifier;
            var hapticStrength = collisionForce / maxCollisionForce;
            VRTK_ControllerHaptics.TriggerHapticPulse(controllerReference, hapticStrength, 0.2f, 0.01f);
        }
        else
        {
            collisionForce = collision.relativeVelocity.magnitude * impactMagnifier;
        }
        Timer.Register(1f, () => { this.hasCollided = false; });
    }

}


