using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using VRKeys;

public class KeyboardOut : MonoBehaviour
{
    private InputField temp;
    public Keyboard keyboard;
    private bool isInput;
    private void Update()
    {
        temp = gameObject.GetComponent<InputField>();
        if (temp.isFocused && !isInput)
        {
            isInput = true;
            keyboard.Enable();
            keyboard.OnUpdate.AddListener(HandleUpdate);
            keyboard.OnSubmit.AddListener(HandleSubmit);
            keyboard.OnCancel.AddListener(HandleCancel);
            keyboard.target = temp;
        }
        if(!temp.isFocused)
        {
            isInput = false;
        }
        //else
        //{
        //    keyboard.OnUpdate.RemoveListener(HandleUpdate);
        //    keyboard.OnSubmit.RemoveListener(HandleSubmit);
        //    keyboard.OnCancel.RemoveListener(HandleCancel);
        //    keyboard.Disable();
        //}
    }

    public void HandleUpdate(string text)
    {
        keyboard.HideValidationMessage();
    }

    public void HandleSubmit(string text)
    {
        keyboard.DisableInput();
        keyboard.OnUpdate.RemoveListener(HandleUpdate);
        keyboard.OnSubmit.RemoveListener(HandleSubmit);
        keyboard.OnCancel.RemoveListener(HandleCancel);
        keyboard.Disable();

        //if (!ValidateEmail(text))
        //{
        //    keyboard.ShowValidationMessage("Please enter a valid email address");
        //    keyboard.EnableInput();
        //    return;
        //}

        //StartCoroutine(SubmitEmail(text));
    }

    public void HandleCancel()
    {
        Debug.Log("Cancelled keyboard input!");
        keyboard.OnUpdate.RemoveListener(HandleUpdate);
        keyboard.OnSubmit.RemoveListener(HandleSubmit);
        keyboard.OnCancel.RemoveListener(HandleCancel);
        keyboard.Disable();
    }

}
