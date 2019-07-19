namespace VRTK.Examples
{
    using UnityEngine;
    using VRTK.Highlighters;

    public class rightcontroller : MonoBehaviour
    {
        public VRTK_DestinationMarker pointer;
        public bool logEnterEvent = true;
        public bool logHoverEvent = false;
        public bool logExitEvent = true;
        public bool logSetEvent = true;

        protected virtual void OnEnable()
        {
            pointer = (pointer == null ? GetComponent<VRTK_DestinationMarker>() : pointer);

            if (pointer != null)
            {
                pointer.DestinationMarkerEnter += DestinationMarkerEnter;
                pointer.DestinationMarkerHover += DestinationMarkerHover;
                pointer.DestinationMarkerExit += DestinationMarkerExit;
                pointer.DestinationMarkerSet += DestinationMarkerSet;
            }
            else
            {
                VRTK_Logger.Error(VRTK_Logger.GetCommonMessage(VRTK_Logger.CommonMessageKeys.REQUIRED_COMPONENT_MISSING_FROM_GAMEOBJECT, "VRTKExample_PointerObjectHighlighterActivator", "VRTK_DestinationMarker", "the Controller Alias"));
            }
        }

        protected virtual void OnDisable()
        {
            if (pointer != null)
            {
                pointer.DestinationMarkerEnter -= DestinationMarkerEnter;
                pointer.DestinationMarkerHover -= DestinationMarkerHover;
                pointer.DestinationMarkerExit -= DestinationMarkerExit;
                pointer.DestinationMarkerSet -= DestinationMarkerSet;
            }
        }

        protected virtual void DestinationMarkerEnter(object sender, DestinationMarkerEventArgs e)
        {
            enter(e.target);
            if (logEnterEvent)
            {
                DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "POINTER ENTER", e.target, e.raycastHit, e.distance, e.destinationPosition);
            }
        }

        private void DestinationMarkerHover(object sender, DestinationMarkerEventArgs e) 
        {
            hover(e.target);
            if (logHoverEvent)
            {
                DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "POINTER HOVER", e.target, e.raycastHit, e.distance, e.destinationPosition);
            }
        }

        protected virtual void DestinationMarkerExit(object sender, DestinationMarkerEventArgs e)
        {
            exit(e.target);
            if (logExitEvent)
            {
                DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "POINTER EXIT", e.target, e.raycastHit, e.distance, e.destinationPosition);
            }
        }

        protected virtual void DestinationMarkerSet(object sender, DestinationMarkerEventArgs e)
        {
            interact(e.target);
            if (logSetEvent)
            {
                DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "POINTER SET", e.target, e.raycastHit, e.distance, e.destinationPosition);
            }
        }

        protected virtual void interact(Transform target)
        {
            cartTelController cart = null;
            poolInteract pool = null;
            ashInteract ash = null;
            boilInteract boil = null;
            pailInteract pail = null;
            tubInteract tub = null;
            pressInteract desk = null;
            cutInteract knife = null;
            if (target.tag.Equals("cart"))
                cart = (target != null ? target.GetComponent<cartTelController>() : null);
            if (target.tag.Equals("pool"))
                pool = (target != null ? target.GetComponent<poolInteract>() : null);
            if (target.tag.Equals("ash"))
                ash = (target != null ? target.GetComponent<ashInteract>() : null);
            if (target.tag.Equals("boil"))
                boil = (target != null ? target.GetComponent<boilInteract>() : null);
            if (target.tag.Equals("pail"))
                pail = (target != null ? target.GetComponent<pailInteract>() : null);
            if (target.tag.Equals("tub"))
                tub = (target != null ? target.GetComponent<tubInteract>() : null);
            if (target.tag.Equals("desk"))
                desk = (target != null ? target.GetComponent<pressInteract>() : null);
            if (target.tag.Equals("cut"))
                knife = (target != null ? target.GetComponent<cutInteract>() : null);
            if (cart != null)
            {
                cart.Point();
            }
            if(pool != null)
            {
                pool.steep();
            }
            if (ash != null)
            {
                ash.dry();
            }
            if(boil != null)
            {
                boil.boil();
            }
            if(pail != null)
            {
                pail.pail();
            }
            if (tub != null)
            {
                tub.filter();
            }
            if(desk != null)
            {
                desk.press();
            }
            if (knife != null)
            {
                knife.cut();
            }
        }

        protected virtual void hover(Transform target)
        {

        }

        protected virtual void enter(Transform target)
        {

        }

        protected virtual void exit(Transform target)
        {

        }

        protected virtual void DebugLogger(uint index, string action, Transform target, RaycastHit raycastHit, float distance, Vector3 tipPosition)
        {
            string targetName = (target ? target.name : "<NO VALID TARGET>");
            string colliderName = (raycastHit.collider ? raycastHit.collider.name : "<NO VALID COLLIDER>");
            VRTK_Logger.Info("Controller on index '" + index + "' is " + action + " at a distance of " + distance + " on object named [" + targetName + "] on the collider named [" + colliderName + "] - the pointer tip position is/was: " + tipPosition);
        }
    }
}
