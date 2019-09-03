using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class hammer : VRTK_InteractableObject
{
    private float impactMagnifier = 120f;
    private float collisionForce = 0f;
    private float maxCollisionForce = 100f;
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
        if (VRTK_ControllerReference.IsValid(controllerReference) && IsGrabbed())
        {
            collisionForce = VRTK_DeviceFinder.GetControllerVelocity(controllerReference).magnitude * impactMagnifier;
            var hapticStrength = collisionForce / maxCollisionForce;
            VRTK_ControllerHaptics.TriggerHapticPulse(controllerReference, hapticStrength, 0.1f, 0.01f);
        }
        else
        {
            collisionForce = collision.relativeVelocity.magnitude * impactMagnifier;
        }
        Timer.Register(1f, () => { this.hasCollided = false; });
    }

}


