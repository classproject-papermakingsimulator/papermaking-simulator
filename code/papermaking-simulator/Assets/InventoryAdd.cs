using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryAdd : MonoBehaviour
{
    public GameObject mtext;
    public GameObject text;

    public void get()
    {

    }

    public void minus()
    {
        String tmp;
        tmp = text.GetComponent<Text>().text;
        int n = int.Parse(tmp);
        if (n != 0)
        {
            n--;
            tmp = n.ToString();
            text.GetComponent<Text>().text = tmp;
        }
        else
            print("原料不足");
    }

    public void add()
    {
        String tmp;
        tmp = text.GetComponent<Text>().text;
        int n = int.Parse(tmp);
        n++;
        tmp = n.ToString();
        text.GetComponent<Text>().text = tmp;
    }
}
