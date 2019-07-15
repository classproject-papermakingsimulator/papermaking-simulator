using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class UImanager : MonoBehaviour
{
    public GameObject telUI;
    public GameObject inventoryUI;
    public GameObject eyecamera;
    public VRTK_ControllerEvents leftController;
    public VRTK_ControllerEvents rightController;
    // Start is called before the first frame update
    protected bool inventoryState;

    protected virtual void OnEnable()
    {
        inventoryState = false;
        RegisterEvents(leftController);
        RegisterEvents(rightController);
    }

    protected virtual void RegisterEvents(VRTK_ControllerEvents events)
    {
        //if (events != null)
        //{
        //    events.ButtonTwoPressed += ButtonTwoPressed;
        //}
    }

    public void InventoryButton()
    {
        inventoryState = !inventoryState;

    }

    protected virtual void Move()
    {
        Transform playArea = VRTK_DeviceFinder.PlayAreaTransform();
        Transform headset = VRTK_DeviceFinder.HeadsetTransform();
        if (playArea != null && headset != null)
        {
            transform.position = new Vector3(headset.position.x, playArea.position.y, headset.position.z);
            telUI.transform.localPosition = headset.forward * 0.5f;
            telUI.transform.localPosition = new Vector3(telUI.transform.localPosition.x, eyecamera.transform.localPosition.y, telUI.transform.localPosition.z);
            Vector3 targetPosition = headset.position;
            targetPosition.y = playArea.transform.position.y;
            telUI.transform.LookAt(targetPosition);
            telUI.transform.localEulerAngles = new Vector3(0f, telUI.transform.localEulerAngles.y + 180, 0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (cartTelController.Instance != null && cartTelController.Instance.getTel)
        {
            telUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Move();
        }
        else
        {
            telUI.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        if (inventoryState)
        {
            inventoryUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Move();
            inventoryUI.transform.position = telUI.transform.position;
            inventoryUI.transform.localEulerAngles = telUI.transform.localEulerAngles;
        }
        else
        {
            inventoryUI.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

    }
}