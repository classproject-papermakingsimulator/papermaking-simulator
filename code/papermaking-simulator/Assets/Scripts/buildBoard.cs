using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buildBoard : MonoBehaviour
{
    public GameObject counter;
    public GameObject board;
    private int count = 0;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        count = (int)counter.transform.position.z;
        if (count != 0)
        {
            board.SetActive(true);
        }
        else
            board.SetActive(false);
    }
}
