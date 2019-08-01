using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paperinteract : MonoBehaviour
{
    public GameObject counter;
    private int count = 0;
    private int newcount = 0;
    public GameObject paper;
    public GameObject paper1;
    public GameObject paper2;
    public GameObject paper3;
    public GameObject paper4;
    public GameObject paper5;
    public GameObject paper6;
    public GameObject paper7;
    public InventoryAdd inventory;


    private void Update()
    {
        count = (int)counter.transform.position.y;

        if (count == 0)
        {
            paper.SetActive(false);
            paper1.SetActive(false);
            paper2.SetActive(false);
            paper3.SetActive(false);
            paper4.SetActive(false);
            paper5.SetActive(false);
            paper6.SetActive(false);
            paper7.SetActive(false);
        }
    }

    public void add()
    {
        switch (count)
        {
            case 0:
                paper.SetActive(true);
                newcount = count + 1;
                counter.transform.position = new Vector3((float)newcount, counter.transform.position.y, counter.transform.position.z);
                break;
            case 1:
                paper1.SetActive(true);
                newcount = count + 1;
                counter.transform.position = new Vector3((float)newcount, counter.transform.position.y, counter.transform.position.z);
                break;
            case 2:
                paper2.SetActive(true);
                newcount = count + 1;
                counter.transform.position = new Vector3((float)newcount, counter.transform.position.y, counter.transform.position.z);
                break;
            case 3:
                paper3.SetActive(true);
                newcount = count + 1;
                counter.transform.position = new Vector3((float)newcount, counter.transform.position.y, counter.transform.position.z);
                break;
            case 4:
                paper4.SetActive(true);
                newcount = count + 1;
                counter.transform.position = new Vector3((float)newcount, counter.transform.position.y, counter.transform.position.z);
                break;
            case 5:
                paper5.SetActive(true);
                newcount = count + 1;
                counter.transform.position = new Vector3((float)newcount, counter.transform.position.y, counter.transform.position.z);
                break;
            case 6:
                paper6.SetActive(true);
                newcount = count + 1;
                counter.transform.position = new Vector3((float)newcount, counter.transform.position.y, counter.transform.position.z);
                break;
            case 7:
                paper7.SetActive(true);
                newcount = count + 1;
                counter.transform.position = new Vector3((float)newcount, counter.transform.position.y, counter.transform.position.z);
                break;
            default:
                newcount = count + 1;
                counter.transform.position = new Vector3((float)newcount, counter.transform.position.y, counter.transform.position.z);
                break;
        }
    }

    public void paperpick()
    {
        for (int i = 0; i < count; ++i)
            inventory.add();
        counter.transform.position = new Vector3(counter.transform.position.x, 0, counter.transform.position.z);
        newcount = 0;
    }
}
