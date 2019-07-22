using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bambooInteract : MonoBehaviour
{
    public InventoryAdd inventory;
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
    public GameObject bamboo;
    public GameObject projectile;
    public Transform body;
    private int num = 10;
    private float r = 0;
    private float d = 0;
    private int direction = 0;
    private int count;
    private bool down;
    public float projectileSpeed = 1000f;

    private void Awake()
    {
        down = false;
        count = 0;
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

    public void cutdown(double v)
    {
        if (v < 10)
            print("low");
        else if (v > 20)
            print("large");
        else if (v > 16)
        {
            count += 2;
            print(count);
            isDown();
        }
        else
        {
            count ++;
            print(count);
            isDown();
        }

    }

    public void pick()
    {
        inventory.add();
        gameObject.SetActive(false);
    }

    public void getOng()
    {
        if(inventory.minus())
        {
            if (projectile != null)
            {
                GameObject projectileClone = Instantiate(projectile, body.transform.position, body.transform.rotation) as GameObject;
                Rigidbody projectileRigidbody = projectile.GetComponent<Rigidbody>();
                if (projectileRigidbody != null)
                {
                    projectileRigidbody.AddForce(projectile.transform.forward * projectileSpeed);
                }
            }
        }
        else
        {
            inventory.add();
        }
    }

    private void isDown()
    {
        if(count == 10 || count == 11)
        {
            count = 0;
            down = true;
        }
    }

    private void Update()
    {
        if(down)
        {
            bamboo.GetComponent<MeshCollider>().enabled = true;
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
    }
}