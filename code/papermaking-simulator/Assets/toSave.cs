using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class toSave : MonoBehaviour
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
        path = Application.dataPath + "/save.txt";
        StreamReader sr = new StreamReader(path, Encoding.Default);  //path为文件路径
        String line;
        int i = 0;
        while ((line = sr.ReadLine()) != null)//按行读取 line为每行的数据
        {
            num[i] = line;
            i++;
        }
        number1.GetComponent<Text>().text = num[0];
        number2.GetComponent<Text>().text = num[1];
        number3.GetComponent<Text>().text = num[2];
        number4.GetComponent<Text>().text = num[3];
        number5.GetComponent<Text>().text = num[4];
        number6.GetComponent<Text>().text = num[5];
        number7.GetComponent<Text>().text = num[6];

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
