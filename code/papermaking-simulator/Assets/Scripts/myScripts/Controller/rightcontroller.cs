namespace VRTK.Examples
{
    using UnityEngine;
    using VRKeys;
    using VRTK.Highlighters;

    public class rightcontroller : MonoBehaviour
    {
        public VRTK_DestinationMarker pointer;
        public VRTK_ControllerEvents events;
        public bool logEnterEvent = true;
        public bool logHoverEvent = false;
        public bool logExitEvent = true;
        public bool logSetEvent = true;
        private bool oneKey = true;

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
            bambooInteract bamboo = null;
            BambooGrab bamboos = null;
            cartTelController cart = null;
            poolInteract pool = null;
            pooledBamboo ashed = null;
            boilInteract boil = null;
            trans pail = null;
            trans mash = null;
            wetpaperinteract wetp = null;
            wetpaper wetsp = null;
            cutInteract knife = null;
            paperinteract dry = null;
            drypaper drypaper = null;
            Board board = null;
            if (target.tag.Equals("cart"))//1
                cart = (target != null ? target.GetComponent<cartTelController>() : null);
            if (target.tag.Equals("pool"))//1
                pool = (target != null ? target.GetComponent<poolInteract>() : null);
            if (target.tag.Equals("ashed"))//1
                ashed = (target != null ? target.GetComponent<pooledBamboo>() : null);
            if (target.tag.Equals("boil"))
                boil = (target != null ? target.GetComponent<boilInteract>() : null);
            if (target.tag.Equals("pail"))//1
                pail = (target != null ? target.GetComponent<trans>() : null);
            if (target.tag.Equals("mash"))//1
                mash = (target != null ? target.GetComponent<trans>() : null);
            if (target.tag.Equals("wet"))//1
                wetp = (target != null ? target.GetComponent<wetpaperinteract>() : null);
            if (target.tag.Equals("wets"))//1
                wetsp = (target != null ? target.GetComponent<wetpaper>() : null);
            if (target.tag.Equals("cut"))
                knife = (target != null ? target.GetComponent<cutInteract>() : null);
            if (target.tag.Equals("bamboo"))
                bamboo = (target != null ? target.GetComponent<bambooInteract>() : null);
            if (target.tag.Equals("bamboos"))
                bamboos = (target != null ? target.GetComponent<BambooGrab>() : null);
            if (target.tag.Equals("drys"))//1
                dry = (target != null ? target.GetComponent<paperinteract>() : null);
            if (target.tag.Equals("drypaper"))//1
                drypaper = (target != null ? target.GetComponent<drypaper>() : null);
            if (target.tag.Equals("Board"))//1
            {
                //print("tag收到");
                board = (target != null ? target.GetComponent<Board>() : null);
            }
               
            if (cart != null)
            {
                cart.Point();
            }
            if(pool != null)
            {
                pool.steep();
            }
            if (ashed != null)
            {
                ashed.pick("Bamboo/staticBamboo2");
            }
            if(boil != null)
            {
                boil.boil();
            }
            if(pail != null)
            {
                pail.pick("mash/Capsule");
            }
            if (mash != null)
            {
                mash.pick("mash/Cube");
            }
            if (wetp != null)
            {
                wetp.pick("wetpaper");
            }
            if(wetsp != null)
            {
                wetsp.paperpick();
            }
            if (knife != null)
            {
                knife.cut();
            }
            if(bamboo != null)
            {
                bamboo.pick();
            }
            if(bamboos != null)
            {
                bamboos.pick("Bamboo/staticBamboo");
            }
            if(dry != null)
            {  
                dry.paperpick();
            }
            if (drypaper != null)
            {
                drypaper.pick("drypaper");
            }
            if(board != null)
            {
                //print("save要跑了");
                board.save();
            }
            
        }

        protected virtual void hover(Transform target)
        {
            if (target.tag.Equals("LetterKey") || target.tag.Equals("Keys"))
            {
                if(events.triggerPressed && oneKey)
                {
                    LetterKey letterkey = null;
                    ShiftKey shiftkey = null;
                    SpaceKey spacekey = null;
                    BackspaceKey backspace = null;
                    ClearKey clearkey = null;
                    EnterKey enterkey = null;
                    CancelKey cancelkey = null;
                    if (target.tag.Equals("LetterKey"))
                    {
                        letterkey = (target != null ? target.GetComponent<LetterKey>() : null);
                    }
                    if (target.name.Equals("Shift"))
                    {
                        shiftkey = (target != null ? target.GetComponent<ShiftKey>() : null);
                    }
                    if (target.name.Equals("Space"))
                    {
                        spacekey = (target != null ? target.GetComponent<SpaceKey>() : null);
                    }
                    if (target.name.Equals("Backspace"))
                    {
                        backspace = (target != null ? target.GetComponent<BackspaceKey>() : null);
                    }
                    if (target.name.Equals("Clear"))
                    {
                        clearkey = (target != null ? target.GetComponent<ClearKey>() : null);
                    }
                    if (target.name.Equals("Enter"))
                    {
                        enterkey = (target != null ? target.GetComponent<EnterKey>() : null);
                    }
                    if (target.name.Equals("Cancel"))
                    {
                        cancelkey = (target != null ? target.GetComponent<CancelKey>() : null);
                    }
                    if (letterkey != null)
                    {
                        letterkey.click();
                    }
                    if(shiftkey != null)
                    {
                        shiftkey.click();
                    }
                    if (spacekey != null)
                    {
                        print("1");
                        spacekey.click();
                    }
                    if (backspace != null)
                    {
                        backspace.click();
                    }
                    if (clearkey != null)
                    {
                        clearkey.click();
                    }
                    if (enterkey != null)
                    {
                        enterkey.click();
                    }
                    if (cancelkey != null)
                    {
                        cancelkey.click();
                    }
                    oneKey = false;
                }
            }
            if(!events.triggerPressed)
            {
                oneKey = true;
            }
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
