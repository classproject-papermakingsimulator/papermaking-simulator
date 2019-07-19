using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationTest : MonoBehaviour
{
    public Transform cubeA;
    public Transform cubeB;
    public Transform end1;
    public Transform end2;
    public Transform end3;
    public Transform end4;
    public Transform end5;
    public Transform end6;
    public Transform end7;
    public Transform end8;
    public Transform start;
    private int num = 10;
    private float r = 0;
    private float d = 0;
    private int direction = 0;

    // Start is called before the first frame update

    void Start()
    {
        cubeA.gameObject.SetActive(false);
        cubeB.gameObject.SetActive(false);
        end1.gameObject.SetActive(false);
        end2.gameObject.SetActive(false);
        end3.gameObject.SetActive(false);
        end4.gameObject.SetActive(false);
        end5.gameObject.SetActive(false);
        end6.gameObject.SetActive(false);
        end7.gameObject.SetActive(false);
        end8.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(num>0) num--;
            Debug.Log(num);
        }
        if (num == 0)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Debug.Log("Q下一次");
                direction = 1;
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                Debug.Log("W一次");
                direction = 2;
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("E一次");
                direction = 3;
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                Debug.Log("R一次");
                direction = 4;
            }
            if (Input.GetKeyDown(KeyCode.T))
            {
                Debug.Log("T一次");
                direction = 5;
            }
            if (Input.GetKeyDown(KeyCode.Y))
            {
                Debug.Log("Y一次");
                direction = 6;
            }
            if (Input.GetKeyDown(KeyCode.U))
            {
                Debug.Log("U一次");
                direction = 7;
            }
            if (Input.GetKeyDown(KeyCode.I))
            {
                Debug.Log("I一次");
                direction = 8;
            }
        }
        if (direction == 1)
        {
            if (r < 3)
            {
                Debug.Log("daodi");

                r += Time.deltaTime;
                Debug.Log(r);
                transform.RotateAround(cubeA.position, cubeA.right, -0.5f);
                Debug.Log("here");
            }
            else if (d < 1)
            {
                d += Time.deltaTime;
                Debug.Log(d);
                transform.position = Vector3.MoveTowards(start.position, end1.position, d);
                Debug.Log("down");

            }


        }



        if (direction == 2)
        {
            if (r < 3)
            {
                Debug.Log("daodi");

                r += Time.deltaTime;
                Debug.Log(r);
                transform.RotateAround(cubeA.position, cubeA.right, 0.5f);
                Debug.Log("here");
            }
            else if (d < 1)
            {
                d += Time.deltaTime;
                Debug.Log(d);
                transform.position = Vector3.MoveTowards(start.position, end2.position, d);
                Debug.Log("down");
            }


        }
        if (direction == 3)
        {
            if (r < 3)
            {
                Debug.Log("daodi");

                r += Time.deltaTime;
                Debug.Log(r);
                transform.RotateAround(cubeA.position, cubeA.forward, -0.5f);
                Debug.Log("here");
            }
            else if (d < 1)
            {
                d += Time.deltaTime;
                Debug.Log(d);
                transform.position = Vector3.MoveTowards(start.position, end3.position, d);
                Debug.Log("down");
            }


        }
        if (direction == 4)
        {
            if (r < 3)
            {
                Debug.Log("daodi");

                r += Time.deltaTime;
                Debug.Log(r);
                transform.RotateAround(cubeA.position, cubeA.forward, 0.5f);
                Debug.Log("here");
            }
            else if (d < 1)
            {
                d += Time.deltaTime;
                Debug.Log(d);
                transform.position = Vector3.MoveTowards(start.position, end4.position, d);
                Debug.Log("down");
            }


        }
        if (direction == 5)
        {
            if (r < 3)
            {
                Debug.Log("daodi");

                r += Time.deltaTime;
                Debug.Log(r);
                transform.RotateAround(cubeB.position, cubeB.right, -0.5f);
                Debug.Log("here");
            }
            else if (d < 1)
            {
                d += Time.deltaTime;
                Debug.Log(d);
                transform.position = Vector3.MoveTowards(start.position, end5.position, d);
                Debug.Log("down");
            }


        }
        if (direction == 6)
        {
            if (r < 3)
            {
                Debug.Log("daodi");

                r += Time.deltaTime;
                Debug.Log(r);
                transform.RotateAround(cubeB.position, cubeB.right, 0.5f);
                Debug.Log("here");
            }
            else if (d < 1)
            {
                d += Time.deltaTime;
                Debug.Log(d);
                transform.position = Vector3.MoveTowards(start.position, end6.position, d);
                Debug.Log("down");
            }


        }
        if (direction == 7)
        {
            if (r < 3)
            {
                Debug.Log("daodi");

                r += Time.deltaTime;
                Debug.Log(r);
                transform.RotateAround(cubeB.position, cubeB.forward, -0.5f);
                Debug.Log("here");
            }
            else if (d < 1)
            {
                d += Time.deltaTime;
                Debug.Log(d);
                transform.position = Vector3.MoveTowards(start.position, end7.position, d);
                Debug.Log("down");
            }


        }
        if (direction == 8)
        {
            if (r < 3)
            {
                Debug.Log("daodi");

                r += Time.deltaTime;
                Debug.Log(r);
                transform.RotateAround(cubeB.position, cubeB.forward, 0.5f);
                Debug.Log("here");
            }
            else if (d < 1)
            {
                d += Time.deltaTime;
                Debug.Log(d);
                transform.position = Vector3.MoveTowards(start.position, end8.position, d);
                Debug.Log("down");
            }


        }


    }
}
