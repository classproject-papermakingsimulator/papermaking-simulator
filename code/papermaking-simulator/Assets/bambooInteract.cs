﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bambooInteract : MonoBehaviour
{
    public GameObject inventory;
    private int count;

    private void Awake()
    {
        count = 0;
    }

    public void cutdown()
    {
        count++;
        isDown();
    }

    private void isDown()
    {
        if(count == 4)
        {
            count = 0;
            //inventory.??
            gameObject.SetActive(false);
        }
    }
}