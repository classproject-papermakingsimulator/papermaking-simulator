using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class exit : MonoBehaviour
{
    public String[] num = new String[7];
    public GameObject number1;
    public GameObject number2;
    public GameObject number3;
    public GameObject number4;
    public GameObject number5;
    public GameObject number6;
    public GameObject number7;
    private String path;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void aExit()
    {
        path = Application.dataPath + "/save.txt";
        FileStream fs = File.Open(path, FileMode.OpenOrCreate);
        num[0] = number1.GetComponent<Text>().text + "\r\n";
        num[1] = number2.GetComponent<Text>().text + "\r\n";
        num[2] = number3.GetComponent<Text>().text + "\r\n";
        num[3] = number4.GetComponent<Text>().text + "\r\n";
        num[4] = number5.GetComponent<Text>().text + "\r\n";
        num[5] = number6.GetComponent<Text>().text + "\r\n";
        num[6] = number7.GetComponent<Text>().text;
        for(int i = 0;i < 7;++i)
        {
            byte[] map = Encoding.UTF8.GetBytes(num[i].ToString());
            fs.Write(map, 0, map.Length);
        }
        fs.Flush();
        fs.Close();
        Application.Quit();
    }
}
